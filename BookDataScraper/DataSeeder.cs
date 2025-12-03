using System.Text;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace BookDataScraper;

public class DataSeeder
{
    private readonly FindlyDbContext _db;
    private readonly BookParsingService _parser;
    private readonly ImageService _imageService;
    private readonly HtmlWeb _web;

    public DataSeeder(FindlyDbContext db, BookParsingService parser)
    {
        _db = db;
        _parser = parser;
        _imageService = new ImageService();
        _web = new HtmlWeb { OverrideEncoding = Encoding.UTF8 };
    }

    public async Task SeedAsync(List<BookSourceConfig> sources)
    {
        // Відкриваємо потоки читання для всіх джерел
        var readers = sources.Select(s => new 
        { 
            Config = s, 
            Reader = new StreamReader(s.LinksFilePath) 
        }).ToList();

        try
        {
            bool anyMoreLines = true;
            while (anyMoreLines)
            {
                anyMoreLines = false;
                
                // Проходимо по кожному джерелу по черзі (Round-robin), щоб не перевантажувати один сайт
                foreach (var source in readers)
                {
                    if (!source.Reader.EndOfStream)
                    {
                        anyMoreLines = true; // Ще є що читати
                        string link = await source.Reader.ReadLineAsync();
                        
                        if (!string.IsNullOrWhiteSpace(link))
                        {
                            await ProcessLinkAsync(link, source.Config);
                        }
                    }
                }

                if (anyMoreLines)
                {
                    Console.WriteLine("Waiting before next batch...");
                    await Task.Delay(5000); // Пауза між ітераціями
                }
            }
        }
        finally
        {
            // Закриваємо всі рідери
            foreach (var source in readers)
            {
                source.Reader.Dispose();
            }
        }
    }

    private async Task ProcessLinkAsync(string link, BookSourceConfig config)
    {
        try
        {
            Console.WriteLine($"Processing [{config.Name}]: {link}");
            var doc = _web.Load(link);
            
            // Парсимо книгу (використовуємо твій існуючий сервіс)
            var book = await _parser.ParseAndSaveBookAsync(doc, config.Selectors);

            if (book != null)
            {
                // Обробка зображення
                if (book.ImageUrl == null || !book.ImageUrl.StartsWith("/images/"))
                {
                    // Логіка витягування src, якщо парсер не зміг, або якщо це відносний шлях
                    // (Тут спрощено, краще щоб ParseAndSaveBookAsync повертав сирий URL)
                    string rawImageUrl = ExtractRawImageUrl(doc, config);
                     
                    if (!string.IsNullOrEmpty(rawImageUrl))
                    {
                        var localPath = await _imageService.DownloadAndSaveImageAsync(rawImageUrl, book.ISBN_13 ?? book.Id.ToString());
                        if (localPath != null)
                        {
                            book.ImageUrl = localPath;
                        }
                    }
                }

                // Додаємо пропозицію (Offer)
                // Перевіряємо, чи такий офер вже є, щоб уникнути дублікатів
                bool offerExists = await _db.Offers.AnyAsync(o => o.Link == link && o.ShopId == config.ShopId);
                if (!offerExists)
                {
                    await _db.Offers.AddAsync(new Offer 
                    { 
                        BookId = book.Id, 
                        Link = link, 
                        ShopId = config.ShopId 
                    });
                    await _db.SaveChangesAsync();
                    Console.WriteLine($"Saved book and offer from {config.Name}");
                }
                else
                {
                    Console.WriteLine($"Offer already exists for {config.Name}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process {link}: {ex.Message}");
        }
    }

    private string ExtractRawImageUrl(HtmlDocument doc, BookSourceConfig config)
    {
        try
        {
            var node = doc.DocumentNode.SelectSingleNode(config.Selectors.ImagePathXPath);
            string src = node?.Attributes["src"]?.Value;
            
            if (string.IsNullOrEmpty(src)) return null;

            // Специфічна логіка для KSD (додавання домену)
            if (config.Name == "KSD" && !src.StartsWith("http"))
            {
                return "https://ksd.ua" + src;
            }
            
            return src;
        }
        catch
        {
            return null;
        }
    }
}
using FindlyDAL.DB;
using FindlyDAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BookDataScraper;

public class DataSeeder
{
    private readonly FindlyDbContext _db;
    private readonly BookParsingService _parser;
    private readonly ImageService _imageService;
    private readonly HtmlWeb _web;

    public DataSeeder(FindlyDbContext db, BookParsingService parser, ImageService imageService)
    {
        _db = db;
        _parser = parser;
        _imageService = imageService;
        _web = new HtmlWeb { OverrideEncoding = Encoding.UTF8 };
    }

    public async Task SeedAsync(List<ShopConfig> shops)
    {
        // Відкриваємо StreamReader для кожного магазину
        var readers = shops.Select(shop => new
        {
            Config = shop,
            Reader = new StreamReader(shop.LinksFilePath)
        }).ToList();

        try
        {
            bool active = true;
            while (active)
            {
                active = false;

                foreach (var item in readers)
                {
                    if (!item.Reader.EndOfStream)
                    {
                        active = true;
                        string link = (await item.Reader.ReadLineAsync())?.Trim();

                        if (!string.IsNullOrEmpty(link))
                        {
                            await ProcessLinkAsync(link, item.Config);
                        }
                    }
                }

                if (active)
                {
                    Console.WriteLine("--- Waiting 5 seconds before next batch ---");
                    await Task.Delay(5000);
                }
            }
        }
        finally
        {
            foreach (var item in readers)
            {
                item.Reader.Dispose();
            }
        }
    }

    private async Task ProcessLinkAsync(string link, ShopConfig config)
    {
        Console.WriteLine($"Processing [{config.Name}]: {link}");
        try
        {
            var doc = _web.Load(link);
            
            var book = await _parser.ParseAndSaveBookAsync(doc, config.Selectors);

            if (book == null) return;
            
            if (string.IsNullOrEmpty(book.ImageUrl))
            {
                string rawImgUrl = ExtractImageUrl(doc, config);
                if (!string.IsNullOrEmpty(rawImgUrl))
                {
                    string? localPath = (await _imageService.DownloadAndSaveImageAsync(rawImgUrl, book.ISBN_13));
                    if (localPath != null)
                    {
                        book.ImageUrl = localPath;
                        _db.Books.Update(book);
                    }
                }
            }
            
            bool offerExists = await _db.Offers.AnyAsync(o => o.BookId == book.Id && o.ShopId == config.ShopId);
            
            if (!offerExists)
            {
                await _db.Offers.AddAsync(new Offer
                {
                    BookId = book.Id,
                    Link = link,
                    ShopId = config.ShopId
                });
                Console.WriteLine($" -> Offer added for {config.Name}");
            }
            else
            {
                Console.WriteLine($" -> Offer already exists.");
            }
            
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing {link}: {e.Message}");
        }
    }

    private string ExtractImageUrl(HtmlDocument doc, ShopConfig config)
    {
        try
        {
            var node = doc.DocumentNode.SelectSingleNode(config.Selectors.ImagePathXPath);
            if (node == null) return null;

            string src = node.Attributes["src"]?.Value;
            if (string.IsNullOrEmpty(src)) return null;
            
            if (!src.StartsWith("http"))
            {
                var baseUrl = config.BaseUrl.TrimEnd('/');
                var relUrl = src.TrimStart('/');
                return $"{baseUrl}/{relUrl}";
            }

            return src;
        }
        catch
        {
            return null;
        }
    }
}

public class ShopConfig
{
    public string Name { get; set; }
    public Guid ShopId { get; set; }
    public string LinksFilePath { get; set; }
    public string BaseUrl { get; set; }
    public BookSelectors Selectors { get; set; }
}
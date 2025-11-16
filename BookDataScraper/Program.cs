using System.Text;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace BookDataScraper;

class Program
{
    static async Task Main(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<FindlyDbContext>();
        optionBuilder.UseSqlServer("Server= localhost; Database= FindlyDb; Trusted_Connection= True; TrustServerCertificate= True");
        FindlyDbContext db = new FindlyDbContext(optionBuilder.Options);
        BookParsingService parser = new(db);

        BookSelectors YakabooSelectors = new BookSelectors()
        {
            AuthorsXPath = "//div[@class='char__title' and contains(., 'Автор')]/../div[@class='char__value']//a//span",
            CoverXPath =
                "//div[@class='char__title' and contains(., 'Тип обкладинки')]/../div[@class='char__value']//span//span",
            ImagePathXPath = "//div[@class='image-container']/img",
            IsbnXPath = "//div[@class='char__title' and contains(., 'ISBN')]/../div[@class='char__value']//span//span",
            PublisherXPath =
                "//div[@class='char__title' and contains(., 'Видавництво')]/../div[@class='char__value']//a//span",
            TitleJsonXPath = "//script[@data-vmid=\"ProductJsonLd\"]",
        };
        BookSelectors KSDSelectors = new BookSelectors()
        {
            AuthorsXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Автор')]/../div/a",
            CoverXPath =
                "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Обкладинка')]/../div",
            ImagePathXPath = "//div[@class='mui-1n73pgb-product-image__image-container']//img",
            IsbnXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'ISBN')]/../div",
            PublisherXPath =
                "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Видавництво')]/../a",
            TitleJsonXPath = "//script[@type=\"application/ld+json\"]",
        };
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        using var yakabooReader = new StreamReader("YakabooLinks.txt");
        using var ksdReader = new StreamReader("KsdLinks.txt");
        HtmlDocument yakabooDoc;
        HtmlDocument ksdDoc;
        while (!yakabooReader.EndOfStream || !ksdReader.EndOfStream)
        {
            if (!yakabooReader.EndOfStream)
            {
                string link = yakabooReader.ReadLine() !;
                yakabooDoc = web.Load(link);
                var yakabooBook = await parser.ParseAndSaveBookAsync(yakabooDoc, YakabooSelectors);
                if (yakabooBook is not null)
                {
                    if (yakabooBook.ImageUrl is null)
                    {
                        try
                        {
                            yakabooBook.ImageUrl = string.Concat(yakabooDoc.DocumentNode.SelectNodes(YakabooSelectors.ImagePathXPath)[0].Attributes["src"].Value);
                            await GetAndSaveImage(yakabooBook);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(yakabooBook.ImageUrl);
                            Console.WriteLine(e);
                        }
                    }
                    await db.Offers.AddAsync(new Offer { BookId = yakabooBook.Id, Link = link, ShopId = Guid.Parse("d985a44b-477e-476d-9387-b816c21190d0")});
                    await db.SaveChangesAsync();
                    Console.WriteLine("Yakaboo book saved");
                }
            }

            if (!ksdReader.EndOfStream)
            {
                string link = ksdReader.ReadLine() !;
                ksdDoc = web.Load(link);
                var ksdBook = await parser.ParseAndSaveBookAsync(ksdDoc, KSDSelectors);
                if (ksdBook is not null)
                {
                    if (ksdBook.ImageUrl is null)
                    {
                        try
                        {
                            ksdBook.ImageUrl = string.Concat("https://ksd.ua",
                                ksdDoc.DocumentNode.SelectNodes(KSDSelectors.ImagePathXPath)[0].Attributes["src"].Value);
                            await GetAndSaveImage(ksdBook);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(ksdBook.ImageUrl);
                            Console.WriteLine(e);
                        }
                    }
                    await db.Offers.AddAsync(new Offer { BookId = ksdBook.Id, Link = link, ShopId = Guid.Parse("071e893b-bcda-4c3e-8721-d7205c348db5")});
                    await db.SaveChangesAsync();
                    Console.WriteLine("ksd book saved");
                }
            }
            await Task.Delay(5000);
        }

    }
    public static async Task GetAndSaveImage(Book book)
    {
        using var client = new HttpClient();
        byte[] imageBytes = await client.GetByteArrayAsync(book.ImageUrl);
        string extension = Path.GetExtension(new Uri(book.ImageUrl !).AbsolutePath);
        string newFileName = book.ISBN_13 + "_image" + extension;
        string destinationFolder = "C:\\study\\5 sem\\CourseWork\\Findly\\FindlyAPI\\FindlyAPI\\wwwroot\\images\\";
        string destinationPath = Path.Combine(destinationFolder, newFileName);
        await File.WriteAllBytesAsync(destinationPath, imageBytes);
        book.ImageUrl = "/images/" + newFileName;
    }
}
//Yakaboo parser data
// string authors = "//div[@class='char__title' and contains(., 'Автор')]/../div[@class='char__value']//a//span";
// string cover = "//div[@class='char__title' and contains(., 'Тип обкладинки')]/../div[@class='char__value']//span//span";
// string publisher= "//div[@class='char__title' and contains(., 'Видавництво')]/../div[@class='char__value']//a//span";
// string price = "//script[@data-vmid=\"ProductJsonLd\"]";
// string ISBN = "//div[@class='char__title' and contains(., 'ISBN')]/../div[@class='char__value']//span//span";
//string title = "//script[@data-vmid=\"ProductJsonLd\"]";
// string productStatus = "//span[@class='ui-shipment-status__text']";
//string imagePath = "//div[@class='image-container']/img";

//KSD parser data
// string authors = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Автор')]/../div/a";
// string cover = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Обкладинка')]/../div";
// string publisher= "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Видавництво')]/../a";
// string price = "//script[@data-vmid=\"ProductJsonLd\"]";
// string ISBN = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'ISBN')]/../div";
// string title = "script[type=\"application/ld+json\"";
// string productStatus = "//div[@class='ui-chip-status--size-small ui-chip-status--variant-available mui-1b75yts-ui-chip-status']/span";
//string imagePath = "//div[@class='mui-1n73pgb-product-image__image-container']//img";



//how I parsed links

// /*var nodes = doc.DocumentNode.SelectNodes("//div[@class='category-card category-layout']/a")
//             .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value)); */ // зчитування Yakaboo.ua, page number < 5
// /*var nodes = doc.DocumentNode.SelectNodes("//a[@class='ui-catalog-card--variant-default mui-1i20r6w-ui-catalog-card']")
//     .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value)); */ // зчитування KSD.ua
// string firstPartOfPath = "https://www.yakaboo.ua";
// //string firstPartOfPath = "https://ksd.ua";
// //ksd url = $"https://ksd.ua/books/klasychna-literatura/page-{i}" page number < 15;
// string url;
// HtmlWeb web = new HtmlWeb();
// web.OverrideEncoding = Encoding.UTF8;
// for (int i = 1; i < 5; i++)
// {
//     url = $"https://www.yakaboo.ua/ua/knigi/hudozhestvennaja-literatura/klassicheskaja-proza.html?book_publication=Bumazhnaja&book_publisher=Knizhnyj_klub_Klub_semejnogo_dosuga_&p={i}";
//     var doc = web.Load(url);
//     var nodes = doc.DocumentNode.SelectNodes("//div[@class='category-card category-layout']/a")
//         .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value));
//     File.AppendAllLines("YakabooLinks.txt", nodes);
//     await Task.Delay(5000);
// } 
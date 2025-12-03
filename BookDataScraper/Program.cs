using FindlyDAL.DB;
using Microsoft.EntityFrameworkCore;

namespace BookDataScraper;

class Program
{
    static async Task Main(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<FindlyDbContext>();
        optionBuilder.UseSqlServer("Server=localhost; Database=FindlyDb; Trusted_Connection=True; TrustServerCertificate=True");
        
        using var context = new FindlyDbContext(optionBuilder.Options);

        string imagePath = @"C:\study\5 sem\CourseWork\Findly\FindlyAPI\FindlyAPI\wwwroot\images\";
        
        var parserService = new BookParsingService(context);
        var imageService = new ImageService(imagePath);
        var seeder = new DataSeeder(context, parserService, imageService);
        
        var shops = new List<ShopConfig>
        {
            new ShopConfig
            {
                Name = "Yakaboo",
                ShopId = Guid.Parse("d985a44b-477e-476d-9387-b816c21190d0"),
                LinksFilePath = "YakabooLinks.txt",
                BaseUrl = "https://www.yakaboo.ua",
                Selectors = new BookSelectors
                {
                    AuthorXPath = "//div[@class='char__title' and contains(., 'Автор')]/../div[@class='char__value']//a//span[1]",
                    AuthorsXPath = "//div[@class='char__title' and contains(., 'Автор')]/../div[@class='char__value']//a//span[1]",
                    CoverXPath = "//div[@class='char__title' and contains(., 'Тип обкладинки')]/../div[@class='char__value']//span//span",
                    ImagePathXPath = "//div[@class='image-container']/img",
                    IsbnXPath = "//div[@class='char__title' and contains(., 'ISBN')]/../div[@class='char__value']//span//span",
                    PublisherXPath = "//div[@class='char__title' and contains(., 'Видавництво')]/../div[@class='char__value']//a//span",
                    TitleJsonXPath = "//script[@data-vmid=\"ProductJsonLd\"]",
                }
            },
            new ShopConfig
            {
                Name = "KSD",
                ShopId = Guid.Parse("071e893b-bcda-4c3e-8721-d7205c348db5"),
                LinksFilePath = "KsdLinks.txt",
                BaseUrl = "https://ksd.ua",
                Selectors = new BookSelectors
                {
                    AuthorXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Автор')]/../div/a",
                    AuthorsXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Автор')]/../div/div",
                    CoverXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Обкладинка')]/../div",
                    ImagePathXPath = "//div[@class='mui-1n73pgb-product-image__image-container']//img",
                    IsbnXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'ISBN')]/../div",
                    PublisherXPath = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Видавництво')]/../a",
                    TitleJsonXPath = "//script[@type=\"application/ld+json\"]",
                }
            }
        };

        Console.WriteLine("Starting seeding process...");
        await seeder.SeedAsync(shops);
        Console.WriteLine("Done.");
    }
}
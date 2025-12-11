using System.Text;
using HtmlAgilityPack;

namespace BookDataScraper;

public class LinkParser
{
    public async Task RunParsingAsync()
    {
        await ParseAndSaveLinksAsync(
            baseUrl: "https://www.yakaboo.ua",
            urlTemplate: "https://www.yakaboo.ua/ua/knigi/hudozhestvennaja-literatura/klassicheskaja-proza.html?book_publication=Bumazhnaja&book_publisher=Knizhnyj_klub_Klub_semejnogo_dosuga_&p={0}",
            xPath: "//div[@class='category-card category-layout']/a",
            fileName: "YakabooLinks.txt",
            maxPages: 4
        );
        
        await ParseAndSaveLinksAsync(
            baseUrl: "https://ksd.ua",
            urlTemplate: "https://ksd.ua/books/klasychna-literatura/page-{0}",
            xPath: "//a[@class='ui-catalog-card--variant-default mui-1i20r6w-ui-catalog-card']", // Трохи спростив клас для надійності
            fileName: "KsdLinks.txt",
            maxPages: 14
        );
    }

    public async Task ParseAndSaveLinksAsync(string baseUrl, string urlTemplate, string xPath, string fileName, int maxPages)
    {
        var web = new HtmlWeb
        {
            OverrideEncoding = Encoding.UTF8
        };

        Console.WriteLine($"Починаю парсинг у файл: {fileName}...");

        for (int i = 1; i <= maxPages; i++)
        {
            try
            {
                string currentUrl = string.Format(urlTemplate, i);
                
                var doc = web.Load(currentUrl);
                var nodeCollection = doc.DocumentNode.SelectNodes(xPath);
                
                if (nodeCollection != null && nodeCollection.Count > 0)
                {
                    var links = nodeCollection
                        .Select(node =>
                        {
                            var href = node.Attributes["href"]?.Value;
                            if (string.IsNullOrEmpty(href)) return null;

                            // Якщо посилання вже повне (починається з http), не додаємо baseUrl
                            return href.StartsWith("http") ? href : string.Concat(baseUrl, href);
                        })
                        .Where(link => !string.IsNullOrEmpty(link));

                    await File.AppendAllLinesAsync(fileName, links);
                    Console.WriteLine($"Сторінка {i} оброблена. Знайдено: {nodeCollection.Count}");
                }
                else
                {
                    Console.WriteLine($"На сторінці {i} нічого не знайдено (або хибний XPath).");
                }
                
                await Task.Delay(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка на сторінці {i}: {ex.Message}");
            }
        }
        
        Console.WriteLine($"Завершено. Результат у {fileName}");
        Console.WriteLine(new string('-', 20));
    }
}
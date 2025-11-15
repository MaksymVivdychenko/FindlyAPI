using System.Text;
using HtmlAgilityPack;

namespace BookDataScraper;

class Program
{
    static async Task Main(string[] args)
    {
        /*var nodes = doc.DocumentNode.SelectNodes("//div[@class='category-card category-layout']/a")
            .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value)); */ // зчитування Yakaboo.ua, page number < 5
        /*var nodes = doc.DocumentNode.SelectNodes("//a[@class='ui-catalog-card--variant-default mui-1i20r6w-ui-catalog-card']")
            .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value)); */ // зчитування KSD.ua
        string firstPartOfPath = "https://www.yakaboo.ua";
        //string firstPartOfPath = "https://ksd.ua";
        //ksd url = $"https://ksd.ua/books/klasychna-literatura/page-{i}" page number < 15;
        string url;
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        for (int i = 1; i < 5; i++)
        {
            url = $"https://www.yakaboo.ua/ua/knigi/hudozhestvennaja-literatura/klassicheskaja-proza.html?book_publication=Bumazhnaja&book_publisher=Knizhnyj_klub_Klub_semejnogo_dosuga_&p={i}";
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='category-card category-layout']/a")
                .Select( q => string.Concat(firstPartOfPath ,q.Attributes["href"].Value));
            File.AppendAllLines("YakabooLinks.txt", nodes);
            await Task.Delay(5000);
        }
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

//KSD parser data
// string authors = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Автор')]/../div/a";
// string cover = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Обкладинка')]/../div";
// string publisher= "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'Видавництво')]/../a";
// string price = "//script[@data-vmid=\"ProductJsonLd\"]";
// string ISBN = "//p[@class='MuiTypography-root MuiTypography-subtitle1 mui-niuy0m-spec-name' and contains(., 'ISBN')]/../div";
// string title = "script[type=\"application/ld+json\"";
// string productStatus = "//div[@class='ui-chip-status--size-small ui-chip-status--variant-available mui-1b75yts-ui-chip-status']/span";
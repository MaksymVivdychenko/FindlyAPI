using System.Text;
using System.Text.Json.Nodes;
using FindlyDAL.Enums;
using HtmlAgilityPack;

namespace FindlyScrapper;

public class ScrapperService : IScrapper
{
    public decimal GetPrice(string url, string nodePath, ParserType parserType)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        var doc = web.Load(url);

        switch (parserType)
        {
            case ParserType.JsonLd:
                return GetPriceJSONld(doc, nodePath);
                break;
            case ParserType.Node:
                return GetPriceNode(doc, nodePath);
                break;
            default:
                return -1;
                break;
                
        }
    }

    private decimal GetPriceJSONld(HtmlDocument doc, string nodePath)
    {
        var jsonLd = doc.DocumentNode
            .SelectSingleNode(nodePath)
            .InnerText;
        return (decimal)JsonNode.Parse(jsonLd)["offers"]["price"];
    }
    
    private decimal GetPriceNode(HtmlDocument doc, string nodePath)
    {
        return (decimal) Decimal.Parse(doc.DocumentNode
            .SelectSingleNode(nodePath)
            .InnerText);
    }
}
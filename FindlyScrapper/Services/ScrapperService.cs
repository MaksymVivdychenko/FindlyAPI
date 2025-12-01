using System.Text;
using System.Text.Json.Nodes;
using FindlyDAL.Enums;
using HtmlAgilityPack;

namespace FindlyScrapper.Services;

public class ScrapperService : IScrapper
{
    public decimal GetPrice(HtmlDocument doc, string nodePath)
    {
        var jsonLd = doc.DocumentNode
            .SelectSingleNode(nodePath)
            .InnerText;
        return (decimal)JsonNode.Parse(jsonLd) !["offers"] !["price"] !;
    }
    
    public bool GetAvailability(HtmlDocument doc, string nodePath)
    {
        var jsonLd = doc.DocumentNode
            .SelectSingleNode(nodePath)
            .InnerText;
        return JsonNode.Parse(jsonLd) ! ["offers"] !["availability"] !.ToString().Contains("InStock");
    }
}
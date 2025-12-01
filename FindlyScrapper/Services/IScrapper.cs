using FindlyDAL.Enums;
using HtmlAgilityPack;

namespace FindlyScrapper;

public interface IScrapper
{
    public decimal GetPrice(HtmlDocument doc, string nodePath);
    public bool GetAvailability(HtmlDocument doc, string nodePath);
}
using FindlyDAL.Enums;

namespace FindlyScrapper;

public interface IScrapper
{
    public decimal GetPrice(string url, string nodePath, ParserType parserType);
}
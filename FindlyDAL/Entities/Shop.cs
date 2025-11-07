namespace FindlyDAL.Entities;

public class Shop : BaseEntity
{
    public string ShopImageUrl { get; set; }
    public string PriceNodePath { get; set; }
    public string Name { get; set; }
    public List<Offer> Offers { get; set; }
}
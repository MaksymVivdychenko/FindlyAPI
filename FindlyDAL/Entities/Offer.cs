namespace FindlyDAL.Entities;

public class Offer : BaseEntity
{
    public int ShopId { get; set; }
    public int BookId { get; set; }
    public string Link { get; set; }
    public decimal Price { get; set; }
    public List<User> Users { get; set; }
}
namespace FindlyDAL.Entities;

public class Offer : BaseEntity
{
    public string Link { get; set; }
    public decimal Price { get; set; }
    
    public Shop Shop { get; set; }
    public int ShopId { get; set; }
    
    public Book Book { get; set; }
    public int BookId { get; set; }
    public List<User> Users { get; set; }
}
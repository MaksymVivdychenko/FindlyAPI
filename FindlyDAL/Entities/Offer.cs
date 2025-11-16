namespace FindlyDAL.Entities;

public class Offer : BaseEntity
{
    public string Link { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    
    public Shop Shop { get; set; }
    public Guid ShopId { get; set; }
    
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    public List<UserLikedOffers> LikedOffers { get; set; }
}
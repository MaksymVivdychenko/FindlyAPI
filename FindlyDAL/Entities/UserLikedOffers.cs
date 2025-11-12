namespace FindlyDAL.Entities;

public class UserLikedOffers
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; }
    public decimal? PriceToNotify { get; set; }
}
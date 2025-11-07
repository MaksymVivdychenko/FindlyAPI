namespace FindlyDAL.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Offer> LikedOffers { get; set; }
}
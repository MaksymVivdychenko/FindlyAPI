namespace FindlyDAL.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public List<Offer> LikedOffers { get; set; }
}
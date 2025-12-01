namespace FindlyDAL.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public List<UserLikedOffers> LikedOffers { get; set; }
    public List<UserDevices> UserDevicesList { get; set; }
}
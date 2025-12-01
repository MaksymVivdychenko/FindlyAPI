namespace FindlyDAL.Entities;

public class UserDevices : BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public string DeviceToken { get; set; }
}
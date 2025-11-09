namespace FindlyScrapper;

public class UserNotifyService : IUserNotify
{
    public string NotifyUser(Guid userId)
    {
        throw new NotImplementedException();
    }
}

public interface IUserNotify
{
    public string NotifyUser(Guid userId);
}
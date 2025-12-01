using FindlyScrapper.Dtos;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Options;

namespace FindlyScrapper;

public class UserNotifyService : IUserNotify
{
    private readonly NotificationChannels _settings;

    public UserNotifyService(IOptions<NotificationChannels> settings)
    {
        _settings = settings.Value;
    }
    public async Task NotifyUser(string deviceToken, Guid userId, string title, string body)
    {
        var message = new Message()
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            Android = new AndroidConfig()
            {
                Priority = Priority.High,
                Notification = new AndroidNotification()
                {
                    ChannelId = _settings.HighPriority
                }
            }
        };
    }
}

public interface IUserNotify
{
    public Task NotifyUser(string deviceToken, Guid userId, string title, string body);
}
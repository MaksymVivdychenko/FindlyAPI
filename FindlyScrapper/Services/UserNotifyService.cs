using FindlyDAL.Interfaces;
using FindlyScrapper.Dtos;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Options;

namespace FindlyScrapper;

public class UserNotifyService : IUserNotify
{
    private readonly IUserDevicesRepository _userDevicesRepository;
    private readonly NotificationChannels _settings;

    public UserNotifyService(
        IOptions<NotificationChannels> settings, 
        IUserDevicesRepository userDevicesRepository)
    {
        _userDevicesRepository = userDevicesRepository;
        _settings = settings.Value;
    }

    public async Task NotifyUser(string deviceToken, string title, string body)
    {
        // 1. Валідація вхідних даних
        if (string.IsNullOrEmpty(deviceToken))
        {
            return;
        }

        var message = new Message()
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            // Додаємо Data, щоб клієнт міг обробити клік (опціонально)
            Data = new Dictionary<string, string>()
            {
                { "click_action", "FLUTTER_NOTIFICATION_CLICK" }, // або ваша логіка навігації
            },
            Android = new AndroidConfig()
            {
                Priority = Priority.High,
                Notification = new AndroidNotification()
                {
                    ChannelId = _settings.HighPriority,
                }
            }
        };

        try
        {
            // 2. Відправка
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
        catch (FirebaseMessagingException ex)
        {
            // Перевіряємо, чи токен не "протух"
            if (ex.MessagingErrorCode == MessagingErrorCode.Unregistered || 
                ex.MessagingErrorCode == MessagingErrorCode.InvalidArgument)
            {
                var device = await _userDevicesRepository.FindSingleAsync(q => q.DeviceToken == deviceToken);
                if (device != null)
                {
                    await _userDevicesRepository.DeleteAsync(device);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}

public interface IUserNotify
{
    public Task NotifyUser(string deviceToken, string title, string body);
}
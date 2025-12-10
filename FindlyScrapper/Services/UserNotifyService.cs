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

    public async Task NotifyUser(List<string> deviceTokens, string title, string body, string offerUrl)
    {
        List<Message> messages = new List<Message>();
        foreach (var deviceToken in deviceTokens)
        {
            var message = new Message()
            {
                Token = deviceToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },

                Data = new Dictionary<string, string>()
                {
                    {"offer_url", offerUrl}
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
        
            messages.Add(message);
        }

        try
        {
            // 2. Відправка
            var batchResponse = await FirebaseMessaging.DefaultInstance.SendEachAsync(messages);
            for (int i = 0; i < batchResponse.Responses.Count; i++)
            {
                var returnValue = batchResponse.Responses[i];
                if (!returnValue.IsSuccess)
                {
                    var ex = returnValue.Exception.MessagingErrorCode;
                    if (ex == MessagingErrorCode.Unregistered
                        || ex == MessagingErrorCode.InvalidArgument)
                    {
                        var device =
                            await _userDevicesRepository
                                .FindSingleAsync(q => q.DeviceToken == deviceTokens[i]);
                        if (device != null)
                        {
                            await _userDevicesRepository.DeleteAsync(device);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Global Firebase Error: {ex.Message}");
        }
    }
}

public interface IUserNotify
{
    public Task NotifyUser(List<string> deviceTokens, string title, string body, string offerUrl);
}
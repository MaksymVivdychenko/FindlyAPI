using System.Runtime.CompilerServices;
using System.Text;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Enums;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace FindlyScrapper;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.OutputEncoding = Encoding.UTF8;
        using var timer = new PeriodicTimer(TimeSpan.FromHours(24));
        
        try
        {
            do
            {
                
                Console.WriteLine("Виконується щоденне завдання...");

                using (var scope = _scopeFactory.CreateScope())
                {
                    HtmlWeb web = new();
                    var dbContext = scope.ServiceProvider.GetRequiredService<FindlyDbContext>();
                    var scrapper = scope.ServiceProvider.GetRequiredService<IScrapper>();
                    var notifier = scope.ServiceProvider.GetRequiredService<IUserNotify>();
                    await TestNotification(dbContext, notifier);

                    List<Offer> offers = dbContext.Offers
                        .Include(q => q.LikedOffers)
                        .Include(q => q.Shop)
                        .ToList();
                    var ksdOffers = offers.Where(q => q.Shop.Name == "ksd.ua").ToList();
                    var yakabooOffers = offers.Where(q => q.Shop.Name == "Yakaboo.ua").ToList();
                    for (int i = 0; i < ksdOffers.Count || i < yakabooOffers.Count; i++)
                    {
                        if (ksdOffers.Count > i)
                        {
                            HtmlDocument doc = web.Load(ksdOffers[i].Link);
                            ksdOffers[i].IsAvailable = scrapper.GetAvailability(doc, ksdOffers[i].Shop.JsonLdPath);
                            var newPrice = scrapper.GetPrice(doc, ksdOffers[i].Shop.JsonLdPath);
                            foreach (var likedOffer in ksdOffers[i].LikedOffers)
                            {
                                if (likedOffer.PriceToNotify >= newPrice)
                                {
                                    var deviceTokens = await dbContext.Users.Where(q => q.LikedOffers.Contains(likedOffer))
                                        .SelectMany(q => q.UserDevicesList).Select(q => q.DeviceToken).ToListAsync();
                                    await notifier.NotifyUser(deviceTokens[0], "Акція", "На товар наступила акція");
                                }
                            }
                            ksdOffers[i].Price = newPrice;
                            Console.WriteLine("ksd updated");
                        }

                        if (yakabooOffers.Count > i)
                        {
                            HtmlDocument doc = web.Load(yakabooOffers[i].Link);
                            yakabooOffers[i].IsAvailable =
                                scrapper.GetAvailability(doc, yakabooOffers[i].Shop.JsonLdPath);
                            yakabooOffers[i].Price = scrapper.GetPrice(doc, yakabooOffers[i].Shop.JsonLdPath);
                            Console.WriteLine("yakaboo updated");
                        }
                        dbContext.SaveChanges();
                        await Task.Delay(5000);
                    }
                }

                Console.WriteLine("Щоденне завдання завершено.");
            } while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Воркер зупиняється.");
        }
    }

    private async Task ChangeDataForOffer(Offer offer, HtmlWeb web, IUserNotify notifier, IScrapper scrapper, FindlyDbContext dbContext)
    {
        HtmlDocument doc = web.Load(offer.Link);
        offer.IsAvailable = scrapper.GetAvailability(doc, offer.Shop.JsonLdPath);
        var newPrice = scrapper.GetPrice(doc, offer.Shop.JsonLdPath);
        foreach (var likedOffer in offer.LikedOffers)
        {
            if (likedOffer.PriceToNotify >= newPrice)
            {
                var deviceTokens = await dbContext.Users.Where(q => q.LikedOffers.Contains(likedOffer))
                    .SelectMany(q => q.UserDevicesList).Select(q => q.DeviceToken).ToListAsync();
                await notifier.NotifyUser(deviceTokens[0], "Акція", "На товар наступила акція");
            }
        }
        offer.Price = newPrice;
        Console.WriteLine("book updated");
    }

    private async Task TestNotification(FindlyDbContext dbContext, IUserNotify userNotify)
    {
        var tokens = dbContext.UserDevices.Select(q => q.DeviceToken).ToList();
        foreach (var token in tokens)
        {
            await userNotify.NotifyUser(token, "заголовок", "наступила акція");
        }
    }
}
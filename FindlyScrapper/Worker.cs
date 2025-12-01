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
                    //var notifier = scope.ServiceProvider.GetRequiredService<IUserNotify>();

                    List<Offer> offers = dbContext.Offers
                        .Include(q => q.Shop).ToList();
                    var ksdOffers = offers.Where(q => q.Shop.Name == "ksd.ua").ToList();
                    var yakabooOffers = offers.Where(q => q.Shop.Name == "Yakaboo.ua").ToList();
                    for (int i = 0; i < ksdOffers.Count || i < yakabooOffers.Count; i++)
                    {
                        if (ksdOffers.Count > i)
                        {
                            HtmlDocument doc = web.Load(ksdOffers[i].Link);
                            ksdOffers[i].IsAvailable = scrapper.GetAvailability(doc, ksdOffers[i].Shop.JsonLdPath);
                            ksdOffers[i].Price = scrapper.GetPrice(doc, ksdOffers[i].Shop.JsonLdPath);
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
}
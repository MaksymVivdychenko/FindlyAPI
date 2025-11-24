using System.Runtime.CompilerServices;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace FindlyScrapper;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(24));
        
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Виконується щоденне завдання... {time}", DateTimeOffset.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<FindlyDbContext>();
                    var Scrapper = scope.ServiceProvider.GetRequiredService<IScrapper>();
                    var notifier = scope.ServiceProvider.GetRequiredService<IUserNotify>();

                    List<Offer> offers = dbContext.Offers
                        .Include(q => q.Shop).ToList();
                    foreach (var offer in offers)
                    {
                        decimal offerNewPrice = Scrapper.GetPrice(offer.Link,
                            offer.Shop.JsonLdPath, ParserType.JsonLd);
                        
                        
                    }
                }

                _logger.LogInformation("Щоденне завдання завершено.");
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Воркер зупиняється.");
        }
    }
}
using System.Text;
using FindlyDAL.DB;
using FindlyDAL.Entities;
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
                    // Отримуємо сервіси
                    var dbContext = scope.ServiceProvider.GetRequiredService<FindlyDbContext>();
                    var scrapper = scope.ServiceProvider.GetRequiredService<IScrapper>();
                    var notifier = scope.ServiceProvider.GetRequiredService<IUserNotify>();
                    var web = new HtmlWeb();

                    // Завантажуємо офери
                    var offers =  dbContext.Offers
                        .Include(q => q.LikedOffers)
                        .Include(q => q.Shop)
                        .Include(q => q.Book);

                    var ksdOffers = offers.Where(q => q.Shop.Name == "ksd.ua").ToList();
                    var yakabooOffers = offers.Where(q => q.Shop.Name == "Yakaboo.ua").ToList();

                    // Визначаємо максимальну кількість ітерацій
                    int maxCount = Math.Max(ksdOffers.Count, yakabooOffers.Count);

                    for (int i = 0; i < maxCount; i++)
                    {
                        // Обробка KSD, якщо ще є
                        if (i < ksdOffers.Count)
                        {
                            await ProcessOfferAsync(ksdOffers[i], web, scrapper, notifier, dbContext);
                            Console.WriteLine($"[KSD] Offer updated: {ksdOffers[i].Id}");
                        }

                        // Обробка Yakaboo, якщо ще є
                        if (i < yakabooOffers.Count)
                        {
                            await ProcessOfferAsync(yakabooOffers[i], web, scrapper, notifier, dbContext);
                            Console.WriteLine($"[Yakaboo] Offer updated: {yakabooOffers[i].Id}");
                        }

                        // Зберігаємо зміни пачкою (за цю ітерацію) та робимо паузу
                        await dbContext.SaveChangesAsync(stoppingToken);
                        await Task.Delay(5000, stoppingToken);
                    }
                }

                Console.WriteLine("Щоденне завдання завершено.");
            } while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Воркер зупиняється.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критична помилка воркера: {ex.Message}");
        }
    }

    /// <summary>
    /// Винесена спільна логіка: завантаження сторінки, парсинг та сповіщення.
    /// </summary>
    private async Task ProcessOfferAsync(Offer offer, HtmlWeb web, IScrapper scrapper, IUserNotify notifier, FindlyDbContext dbContext)
    {
        try
        {
            HtmlDocument doc = web.Load(offer.Link);
            
            // Оновлюємо доступність та ціну
            offer.IsAvailable = scrapper.GetAvailability(doc, offer.Shop.JsonLdPath);
            var newPrice = scrapper.GetPrice(doc, offer.Shop.JsonLdPath);

            // Перевірка на сповіщення користувачів
            if (offer.LikedOffers != null && offer.LikedOffers.Any())
            {
                foreach (var likedOffer in offer.LikedOffers)
                {
                    if (likedOffer.PriceToNotify >= newPrice)
                    {
                        // Знаходимо токени користувачів, які лайкнули цей офер
                        var deviceTokens = await dbContext.Users
                            .Where(q => q.LikedOffers.Contains(likedOffer))
                            .SelectMany(q => q.UserDevicesList)
                            .Select(q => q.DeviceToken)
                            .ToListAsync();
                        
                        if (deviceTokens.Any())
                        {
                            await notifier.NotifyUser(deviceTokens,
                                $"Знижка на товар \"{offer.Book.Title}\"", $"Ціна на товар \"{offer.Book.Title}\" на сайті {offer.Shop.Name} становить {offer.Price} грн",
                                offer.Link);
                        }
                    }
                }
            }

            offer.Price = newPrice;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при обробці офера {offer.Id} ({offer.Link}): {ex.Message}");
        }
    }
}
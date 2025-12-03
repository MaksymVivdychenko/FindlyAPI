using FindlyDAL.DB;
using FindlyDAL.Interfaces;
using FindlyDAL.Repositories;
using FindlyScrapper;
using FindlyScrapper.Services;
using Microsoft.EntityFrameworkCore;
using FindlyScrapper.Dtos;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = Host.CreateApplicationBuilder(args);
string pathToSharedAppsettings = Path.Combine(builder.Environment.ContentRootPath, "..", "appsettings.shared.json");

// if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions()
    {
        // Вкажіть шлях до вашого JSON файлу
        Credential = GoogleCredential.FromFile("ServiceAccountKey.json")
    });
}
builder.Services.Configure<NotificationChannels>(
    builder.Configuration.GetSection("Firebase"));
builder.Configuration.AddJsonFile(Path.GetFullPath(pathToSharedAppsettings), optional: false, reloadOnChange: true);
builder.Services.AddDbContext<FindlyDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("FindlyDbConnectionString"))
    );

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IScrapper, ScrapperService>();
builder.Services.AddScoped<IUserDevicesRepository, UserDeviceRepository>();
builder.Services.AddScoped<IUserNotify, UserNotifyService>();

var host = builder.Build();
host.Run();
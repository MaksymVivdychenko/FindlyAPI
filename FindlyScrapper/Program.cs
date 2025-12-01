using FindlyDAL.DB;
using FindlyScrapper;
using FindlyScrapper.Services;
using Microsoft.EntityFrameworkCore;
using FindlyScrapper.Dtos;

var builder = Host.CreateApplicationBuilder(args);
string pathToSharedAppsettings = Path.Combine(builder.Environment.ContentRootPath, "..", "appsettings.shared.json");

builder.Services.Configure<NotificationChannels>(
    builder.Configuration.GetSection("Firebase"));
builder.Configuration.AddJsonFile(Path.GetFullPath(pathToSharedAppsettings), optional: false, reloadOnChange: true);
builder.Services.AddDbContext<FindlyDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("FindlyDbConnectionString"))
    );
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IScrapper, ScrapperService>();

var host = builder.Build();
host.Run();
using FindlyDAL.DB;
using FindlyScrapper;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
string pathToSharedAppsettings = Path.Combine(builder.Environment.ContentRootPath, "..", "appsettings.shared.json");

builder.Configuration.AddJsonFile(Path.GetFullPath(pathToSharedAppsettings), optional: false, reloadOnChange: true);
builder.Services.AddDbContext<FindlyDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("FindlyDbConnectionString"))
    );
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
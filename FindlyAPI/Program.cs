using FindlyDAL.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string pathToSharedAppsettings = Path.Combine(builder.Environment.ContentRootPath, "..", "appsettings.shared.json");

builder.Configuration.AddJsonFile(Path.GetFullPath(pathToSharedAppsettings), optional: false, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddDbContext<FindlyDbContext>(
    options => options.UseSqlServer(builder.Configuration
        .GetConnectionString("FindlyDbConnectionString")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
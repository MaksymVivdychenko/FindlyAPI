using System.Reflection;
using System.Text;
using FindlyAPI.Middlewares;
using FindlyBLL.AutoMapperProfiles;
using FindlyBLL.Interfaces;
using FindlyBLL.Services;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using FindlyDAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string pathToSharedAppsettings = Path.Combine(builder.Environment.ContentRootPath, "..", "appsettings.shared.json");

builder.Configuration.AddJsonFile(Path.GetFullPath(pathToSharedAppsettings), optional: false, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<ILikedOfferRepository, LikedOfferRepository>();
builder.Services.AddScoped<IUserDevicesRepository, UserDeviceRepository>();
builder.Services.AddDbContext<FindlyDbContext>(
    options => options.UseSqlServer(builder.Configuration
        .GetConnectionString("FindlyDbConnectionString")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        };
    });
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseExceptionHandlingMiddleware();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
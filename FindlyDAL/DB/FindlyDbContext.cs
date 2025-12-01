using System.Reflection;
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.DB;

public class FindlyDbContext : DbContext
{
    public DbSet<UserDevices> UserDevices { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cover> Cover { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<UserLikedOffers> UserLikedOffers { get; set; }

    public FindlyDbContext(DbContextOptions<FindlyDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}
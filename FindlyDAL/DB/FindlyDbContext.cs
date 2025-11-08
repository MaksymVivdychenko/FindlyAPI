using System.Reflection;
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.DB;

public class FindlyDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cover> Cover { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}
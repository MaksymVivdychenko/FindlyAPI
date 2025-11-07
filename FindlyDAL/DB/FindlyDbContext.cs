using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.DB;

public class FindlyDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
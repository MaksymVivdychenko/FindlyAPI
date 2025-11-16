using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class BookConfiguration : BaseConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);
        builder.HasIndex(q => q.ISBN_13).IsUnique();
        builder.HasOne(q => q.Cover)
            .WithMany(q => q.Books).HasForeignKey(q => q.CoverId);
        builder.HasOne(q => q.Publisher)
            .WithMany(q => q.Books).HasForeignKey(q => q.PublisherId);
        builder.HasMany(q => q.Authors).WithMany(q => q.Books)
            .UsingEntity(
                "authors_books",
                q => q.HasOne(typeof(Author)).WithMany().HasForeignKey("authorId"),
                a => a.HasOne(typeof(Book)).WithMany().HasForeignKey("bookId"));
    }
}
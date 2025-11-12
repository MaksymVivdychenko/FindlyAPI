using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class BookConfiguration : BaseConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);
        builder.ToTable("books");
        builder.HasOne(q => q.Cover)
            .WithMany(q => q.Books).HasForeignKey(q => q.CoverId);
        builder.HasOne(q => q.Publisher)
            .WithMany(q => q.Books).HasForeignKey(q => q.PublisherId);
        builder.HasData(new Book[]
        {
            new Book
            {
                Id = Guid.Parse("26e5c51c-37e3-4c38-8e3e-41332eeaea73"),
                Title = "1984",
                ISBN_13 = "978-966-993-391-1",
                CoverId = Guid.Parse("a9819fd8-65c7-4925-af38-a29c030280f6"),
                PublisherId = Guid.Parse("42e470a1-702a-4994-a7f9-0b7283cc41d6"),
            },
            new Book
            {
            Id = Guid.Parse("00461873-724c-49bf-adde-fe204bce4466"),
            Title = "1984",
            ISBN_13 = "978-617-7910-08-3",
            CoverId = Guid.Parse("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"),
            PublisherId = Guid.Parse("0a20441e-d50a-4da4-b560-a91c4aa69080"),
            },
            new Book
            {
                Id = Guid.Parse("11226951-89aa-4611-9346-11b9aba3d52d"),
                Title = "Дюна",
                ISBN_13 = "978-617-12-7689-5",
                CoverId = Guid.Parse("a9819fd8-65c7-4925-af38-a29c030280f6"),
                PublisherId = Guid.Parse("42e470a1-702a-4994-a7f9-0b7283cc41d6"),
            },
            new Book
            {
            Id = Guid.Parse("c043abf2-91e1-4e68-82bd-8d55c88d7457"),
            Title = "Відьмак. Останнє бажання. Книга 1",
            ISBN_13 = "978-617-12-8351-0",
            CoverId = Guid.Parse("a9819fd8-65c7-4925-af38-a29c030280f6"),
            PublisherId = Guid.Parse("42e470a1-702a-4994-a7f9-0b7283cc41d6"),
            
            }
        });
        builder.HasMany(q => q.Authors).WithMany(q => q.Books)
            .UsingEntity(
                "authors_books",
                q => q.HasOne(typeof(Author)).WithMany().HasForeignKey("authorId"),
                a => a.HasOne(typeof(Book)).WithMany().HasForeignKey("bookId"),
                join =>
                {
                    join.HasData(
                        new
                        {
                            bookId = Guid.Parse("26e5c51c-37e3-4c38-8e3e-41332eeaea73"),
                            authorId = Guid.Parse("a259a396-b950-4d63-8ce9-8b83fa187a8e")
                        },
                        new
                        {
                            bookId = Guid.Parse("00461873-724c-49bf-adde-fe204bce4466"),
                            authorId = Guid.Parse("a259a396-b950-4d63-8ce9-8b83fa187a8e")
                        },
                        new
                        {
                            bookId = Guid.Parse("11226951-89aa-4611-9346-11b9aba3d52d"),
                            authorId = Guid.Parse("391d1568-480f-4164-a10f-2c38a7391858")
                        },
                        new
                        {
                            bookId = Guid.Parse("c043abf2-91e1-4e68-82bd-8d55c88d7457"),
                            authorId = Guid.Parse("7c69abe6-7604-4804-8d45-d5bdde4410d9")
                        });
                });

    }
}
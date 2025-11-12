using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class AuthorConfiguration : BaseConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);
        builder.HasData(new Author[]
        {
            new Author { Id = Guid.Parse("a259a396-b950-4d63-8ce9-8b83fa187a8e"), Name = "Джордж Орвелл" },
            new Author { Id = Guid.Parse("391d1568-480f-4164-a10f-2c38a7391858"), Name = "Френк Герберт" },
            new Author { Id = Guid.Parse("7c69abe6-7604-4804-8d45-d5bdde4410d9"), Name = "Анджей Сапковський" },
        });
    }
}
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class AuthorConfiguration : BaseConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);
    }
}
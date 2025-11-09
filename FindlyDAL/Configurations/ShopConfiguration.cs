using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class ShopConfiguration : BaseConfiguration<Shop>
{
    public override void Configure(EntityTypeBuilder<Shop> builder)
    {
        base.Configure(builder);
        builder.Property(q => q.ParserType).HasConversion<string>();
    }
}
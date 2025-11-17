using FindlyDAL.Entities;
using FindlyDAL.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class ShopConfiguration : BaseConfiguration<Shop>
{
    public override void Configure(EntityTypeBuilder<Shop> builder)
    {
        base.Configure(builder);
        builder.HasData(new[]
        {
            new Shop 
            {
                Id = Guid.Parse("d985a44b-477e-476d-9387-b816c21190d0"),
                Name = "Yakaboo.ua",
                JsonLdPath = "//script[@data-vmid=\"ProductJsonLd\"]",
                ShopImageUrl = ""
            },
            new Shop 
            {
                Id = Guid.Parse("071e893b-bcda-4c3e-8721-d7205c348db5"),
                Name = "ksd.ua",
                JsonLdPath = "//script[@type='application/ld+json']",
                ShopImageUrl = ""
            },
        });
    }
}
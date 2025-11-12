using FindlyDAL.Entities;
using FindlyDAL.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class ShopConfiguration : BaseConfiguration<Shop>
{
    public override void Configure(EntityTypeBuilder<Shop> builder)
    {
        base.Configure(builder);
        builder.Property(q => q.ParserType).HasConversion<string>();
        builder.HasData(new[]
        {
            new Shop 
            {
                Id = Guid.NewGuid(),Name = "Yakaboo.ua",
                ParserType = ParserType.JsonLd,
                PriceNodePath = "//script[@data-vmid=\"ProductJsonLd\"]",
                ShopImageUrl = ""
            },
            new Shop
            {
                Id = Guid.NewGuid(),
                Name = "balka-book.com", ParserType = ParserType.JsonLd,
                PriceNodePath = "//script[@type='application/ld+json']",
                ShopImageUrl = ""
            },
            new Shop 
            {
                Id = Guid.NewGuid(),
                Name = "book24.ua", ParserType = ParserType.Node,
                PriceNodePath = "//div[@class='product-main']//span[@class='price_value']", 
                ShopImageUrl = ""
            },
            new Shop 
            {
                Id = Guid.NewGuid(),
                Name = "ksd.ua", ParserType = ParserType.JsonLd,
                PriceNodePath = "//script[@type='application/ld+json']",
                ShopImageUrl = ""
            },
        });
    }
}
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class OfferConfiguration : BaseConfiguration<Offer>
{
    public override void Configure(EntityTypeBuilder<Offer> builder)
    {
        base.Configure(builder);
        builder.HasOne(q => q.Book).WithMany(q => q.Offers).HasForeignKey(q => q.BookId);
        builder.HasOne(q => q.Shop).WithMany(q => q.Offers).HasForeignKey(q => q.ShopId);
    }
}
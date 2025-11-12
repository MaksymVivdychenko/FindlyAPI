using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class UserLikedOffersConfiguration : IEntityTypeConfiguration<UserLikedOffers>
{
    public void Configure(EntityTypeBuilder<UserLikedOffers> builder)
    {
        builder.HasKey(nameof(UserLikedOffers.UserId),
            nameof(UserLikedOffers.OfferId));
        builder.HasOne(q => q.Offer).WithMany(q => q.LikedOffers)
            .HasForeignKey(q => q.OfferId);
    }
}
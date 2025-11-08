using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("users");
        builder.HasMany(q => q.LikedOffers)
            .WithMany(q => q.Users)
            .UsingEntity(
                "user_liked_offers",
                u => u.HasOne(typeof(Offer))
                    .WithMany()
                    .HasForeignKey("userId"),
                o => o.HasOne(typeof(User))
                    .WithMany()
                    .HasForeignKey("offerId"),
                q => q.HasKey("userId" , "offerId"));
    }
}
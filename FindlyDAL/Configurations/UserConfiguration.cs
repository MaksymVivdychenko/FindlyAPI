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
        builder.HasIndex(q => q.Login).IsUnique();
        builder.HasMany(q => q.LikedOffers)
            .WithOne(q => q.User).HasForeignKey(q => q.UserId);
    }
}
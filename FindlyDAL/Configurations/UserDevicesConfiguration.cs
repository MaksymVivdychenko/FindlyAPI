using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class UserDevicesConfiguration : BaseConfiguration<UserDevices>
{
    public override void Configure(EntityTypeBuilder<UserDevices> builder)
    {
        base.Configure(builder);
        builder.HasOne(q => q.User).WithMany(q => q.UserDevicesList)
            .HasForeignKey(q => q.UserId);
        builder.HasIndex(q => q.DeviceToken).IsUnique();
    }
}
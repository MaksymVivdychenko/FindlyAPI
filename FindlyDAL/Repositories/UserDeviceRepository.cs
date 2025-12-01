using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Repositories;

public class UserDeviceRepository :Repository<UserDevices>, IUserDevicesRepository
{
    public UserDeviceRepository(FindlyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddDeviceToUser(Guid userId, string deviceId)
    {
        var device = await DbContext.UserDevices.SingleOrDefaultAsync(q => q.DeviceToken == deviceId);
        if (device == null)
        {
            DbContext.UserDevices.Add(
                new UserDevices
                {
                    DeviceToken = deviceId,
                    UserId = userId
                });
            await DbContext.SaveChangesAsync();
            return;
        }

        if (device.UserId != userId)
        {
            device.UserId = userId;
            await DbContext.SaveChangesAsync();
        }
    }
}
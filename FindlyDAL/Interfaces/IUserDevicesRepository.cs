using FindlyDAL.Entities;

namespace FindlyDAL.Interfaces;

public interface IUserDevicesRepository : IRepository<UserDevices>
{
    Task AddDeviceToUser(Guid userId, string deviceId);
}
using FindlyBLL.DTOs;

namespace FindlyBLL.Services;

public interface IUserService
{
    Task<Guid> RegisterUser(UserRegDto user);
    Task<Guid> LoginUser(UserRegDto user);
}
using FindlyBLL.DTOs.UserDtos;

namespace FindlyBLL.Interfaces;

public interface IUserService
{
    Task<AuthResponse> RegisterUser(RegisterUserDto registerUser);
    Task<AuthResponse> LoginUser(LoginUserDto registerUser);
    Task<bool> ChangePassword(Guid userId ,UserChangePasswordDto passwords);
}
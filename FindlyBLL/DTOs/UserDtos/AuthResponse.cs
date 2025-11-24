namespace FindlyBLL.DTOs.UserDtos;

public class AuthResponse
{
    required public string Login { get; set; }
    required public string Token { get; set; }
    required public Guid UserId { get; set; }
}
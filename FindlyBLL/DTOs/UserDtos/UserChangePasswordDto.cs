namespace FindlyBLL.DTOs.UserDtos;

public class UserChangePasswordDto
{
    required public string NewPassword { get; set; }
    required public string OldPassword { get; set; }
}
using System.Security.Claims;
using FindlyBLL.DTOs;
using FindlyBLL.DTOs.UserDtos;
using FindlyBLL.Interfaces;
using FindlyBLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUser)
    {
        var response = await _userService.RegisterUser(registerUser);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUser)
    {
        var response = await _userService.LoginUser(loginUser);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await _userService.ChangePassword(userId, dto);
        return Ok(new { message = "Пароль успішно змінено" });
    }
}
using FindlyBLL.DTOs;
using FindlyBLL.Services;
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

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegDto user)
    {
        var id = await _userService.RegisterUser(user);
        return Ok(id);
    }

    [HttpPost("login/")]
    public async Task<IActionResult> LoginUser([FromBody] UserRegDto user)
    {
        var id = await _userService.LoginUser(user);
        return Ok(id);
    }
}
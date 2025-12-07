using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FindlyBLL.DTOs;
using FindlyBLL.DTOs.UserDtos;
using FindlyBLL.Exceptions;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FindlyBLL.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _hasher;
    private readonly FindlyDbContext _context;
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;
    private readonly IUserDevicesRepository _userDevicesRepo;
    private readonly IMapper _mapper;

    public UserService(IPasswordHasher hasher, FindlyDbContext context,
        IUserRepository userRepo , IConfiguration configuration,
        IUserDevicesRepository userDevicesRepo,IMapper mapper)
    {
        _hasher = hasher;
        _context = context;
        _userRepo = userRepo;
        _configuration = configuration;
        _userDevicesRepo = userDevicesRepo;
        _mapper = mapper;
    }

    public async Task<AuthResponse> RegisterUser(RegisterUserDto registerUser)
    {
        if (await _userRepo.IsLoginNotUnique(registerUser.Login))
        {
            
            throw new AuthException("Invalid login");
        }
        
        var userEntity = new User { Login = registerUser.Login, PasswordHash = _hasher.HashPassword(registerUser.Password) };
        await _userRepo.AddAsync(userEntity);
        await _userDevicesRepo.AddDeviceToUser(userEntity.Id, registerUser.DeviceToken);
        return new AuthResponse
        {
            Login = userEntity.Login,
            Token = GenerateJwt(userEntity),
            UserId = userEntity.Id,
        };
    }

    public async Task<AuthResponse> LoginUser(LoginUserDto registerUser)
    {
        var userEntity = (await _userRepo.FindAsync(q => q.Login == registerUser.Login)).FirstOrDefault();
        if(userEntity is null)
        {
            throw new AuthException("Invalid login or password");
        }
        if (!_hasher.VerifyPassword(registerUser.Password, userEntity.PasswordHash))
        {
            throw new AuthException("Invalid login or password");
        }
        await _userDevicesRepo.AddDeviceToUser(userEntity.Id, registerUser.DeviceToken);
        return new AuthResponse
        {
            Login = userEntity.Login,
            Token = GenerateJwt(userEntity),
            UserId = userEntity.Id,
        };
    }
    
    public async Task<bool> ChangePassword(Guid userId ,UserChangePasswordDto passwords)
    {
        var user = await _userRepo.GetByIdAsync(userId);
        if(user is null)
        {
            throw new KeyNotFoundException("User doesn`t exists");
        }

        if (!_hasher.VerifyPassword(passwords.OldPassword, user.PasswordHash))
        {
            throw new UserChangePasswordException("Incorrect old password");
        }

        user.PasswordHash = _hasher.HashPassword(passwords.NewPassword);
        await _userRepo.UpdateAsync(user);
        return true;
    }
    
    private string GenerateJwt(User user)
    {
        var jwtSettings = _configuration.GetSection("jwt");
        var secretKey = jwtSettings["Key"] !;
        var issuer = jwtSettings["Issuer"] !;
        var audience = jwtSettings["Audience"] !;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience, claims: claims, expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
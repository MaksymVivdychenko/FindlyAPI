using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FindlyBLL.DTOs;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FindlyBLL.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _hasher;
    private readonly FindlyDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(IPasswordHasher hasher, FindlyDbContext context, IConfiguration configuration)
    {
        _hasher = hasher;
        _context = context;
        _configuration = configuration;
    }

    public async Task<Guid> RegisterUser(UserRegDto user)
    {
        if (await IsLoginExist(user.Login))
        {
            throw new Exception("Invalid login");
        }
        
        var userEnitity = new User { Login = user.Login, PasswordHash = _hasher.HashPassword(user.Password) };
        await _context.Users.AddAsync(userEnitity);
        await _context.SaveChangesAsync();
        return userEnitity.Id;
    }
    
    public async Task<Guid> LoginUser(UserRegDto user)
    {
        var userEntity = await _context.Users.SingleOrDefaultAsync(q => q.Login == user.Login);
        if(userEntity is null)
        {
            throw new Exception("Incorrect login or password");
        }
        if (!_hasher.VerifyPassword(user.Password, userEntity.PasswordHash))
        {
            throw new Exception("Incorrect login or password");
        }

        return userEntity.Id;
    }

    private async Task<bool> IsLoginExist(string login)
    {
        return await _context.Users.AnyAsync(q => q.Login == login);
    }

    private string GenerateJWT(User user)
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
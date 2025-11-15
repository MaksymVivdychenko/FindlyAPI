namespace FindlyBLL.Services;
using BCrypt.Net;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password,12);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Verify(password, passwordHash);
    }
}
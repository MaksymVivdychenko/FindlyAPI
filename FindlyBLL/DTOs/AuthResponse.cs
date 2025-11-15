namespace FindlyBLL.DTOs;

public class AuthResponse
{
    public string JWT { get; set; }
    public Guid UserId { get; set; }
}
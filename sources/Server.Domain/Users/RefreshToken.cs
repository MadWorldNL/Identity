namespace MadWorldNL.Server.Domain.Users;

public class RefreshToken
{
    public RefreshToken() {}
    
    public RefreshToken(string token, string audience, DateTime expires, string userId)
    {
        Token = token;
        Audience = audience;
        Expires = expires;
        UserId = userId;
    }
    
    public Guid Id { get; set; }
    public string Audience { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public IIdentityUser User { get; set; } = null!;
}
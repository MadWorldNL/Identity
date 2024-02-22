namespace MadWorldNL.Server.Domain.Users.RefreshTokens;

public class Token
{
    public string RefreshToken { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
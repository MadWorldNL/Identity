namespace MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

public class LoginProxyResponse
{
    public bool IsSuccess { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; }
}
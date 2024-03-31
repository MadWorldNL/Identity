namespace MadWorldNL.Clients.Identity.Api.Shared.Authentications;

public class LoginProxyRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
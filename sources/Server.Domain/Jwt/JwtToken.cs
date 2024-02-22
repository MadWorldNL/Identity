namespace MadWorldNL.Server.Domain.Jwt;

public class JwtToken
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset Expires { get; set; }
}
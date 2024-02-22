namespace MadWorldNL.Server.Domain.Jwt;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
}
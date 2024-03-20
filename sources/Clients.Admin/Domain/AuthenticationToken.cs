namespace MadWorldNL.Clients.Admin.Domain;

public class AuthenticationToken
{
    public bool IsSuccess { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public DateTime Expires { get; init; }
}
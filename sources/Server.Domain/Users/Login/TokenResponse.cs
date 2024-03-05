namespace MadWorldNL.Server.Domain.Users.Login;

public class TokenResponse
{
    public bool IsSuccess { get; init; }
    public string Jwt { get; init; } = string.Empty;
    public DateTimeOffset Expires  { get; init; }
    public string RefreshToken { get; init; } = string.Empty;
    
    public string Message { get; set; } = string.Empty;

    public static TokenResponse AccessDenied()
    {
        return new TokenResponse()
        {
            Message = "Access denied",
            Expires = DateTimeOffset.MinValue,
        };
    }
}
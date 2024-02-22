namespace MadWorldNL.Server.Domain.Users.Login;

public class Login
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}
namespace MadWorldNL.Server.Domain.Users.ForgotPasswords;

public class ForgotPasswordResult
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}
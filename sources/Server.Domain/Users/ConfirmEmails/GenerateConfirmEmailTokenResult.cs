namespace MadWorldNL.Server.Domain.Users.ConfirmEmails;

public class GenerateConfirmEmailTokenResult
{
    public bool EmailExists { get; set; }
    public string Token { get; set; } = string.Empty;
}
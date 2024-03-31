namespace MadWorldNL.Clients.Identity.Api.Shared.Settings;

public class IdentitySettings
{
    public const string Entry = "Identity";
    
    public string Audience { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
}
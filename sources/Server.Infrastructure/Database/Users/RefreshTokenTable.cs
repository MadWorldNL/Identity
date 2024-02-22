namespace MadWorldNL.Server.Infrastructure.Database.Users;

public class RefreshTokenTable
{
    public const int MaxLength = 1000;
    
    public Guid Id { get; set; }
    public string Audience { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public virtual IdentityUserExtended User { get; set; } = null!;
}
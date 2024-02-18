namespace MadWorldNL.Server.Domain.Users;

public interface IIdentityUser
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
}
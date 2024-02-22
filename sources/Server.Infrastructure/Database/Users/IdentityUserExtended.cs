using MadWorldNL.Server.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public class IdentityUserExtended : IdentityUser, IIdentityUser
{
    public virtual ICollection<RefreshTokenTable> RefreshTokens { get; } = null!;
}
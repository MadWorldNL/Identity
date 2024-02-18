using MadWorldNL.Server.Infrastructure.Database.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadWorldNL.Server.Infrastructure.Database;

public class UserDbContext : IdentityDbContext<IdentityUserExtended>
{
    public UserDbContext(DbContextOptions options) : base(options) { }
}
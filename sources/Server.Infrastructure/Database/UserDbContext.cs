using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadWorldNL.Server.Infrastructure.Database;

public class UserDbContext : IdentityDbContext<IdentityUserExtended>
{
    public DbSet<RefreshTokenTable> RefreshTokens { get; set; } = null!;
    public UserDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RefreshTokenTableEntityTypeConfiguration());
        
        base.OnModelCreating(builder);
    }
}
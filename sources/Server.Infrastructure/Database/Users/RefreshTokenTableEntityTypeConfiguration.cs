using MadWorldNL.Server.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public class RefreshTokenTableEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenTable>
{
    public void Configure(EntityTypeBuilder<RefreshTokenTable> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Audience)
            .IsRequired()
            .HasMaxLength(RefreshTokenTable.MaxLength);
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(RefreshTokenTable.MaxLength);

        builder.Property(x => x.Expires)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(RefreshTokenTable.MaxLength);
        
        builder
            .Navigation(e => e.User)
            .AutoInclude();

        builder.HasOne<IdentityUserExtended>(t => t.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(u => u.Id);
    }
}
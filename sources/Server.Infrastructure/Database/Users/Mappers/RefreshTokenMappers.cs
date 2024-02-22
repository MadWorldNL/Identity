using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Infrastructure.Database.Users.Mappers;

public static class RefreshTokenMappers
{
    public static RefreshTokenTable ToTable(this RefreshToken refreshToken)
    { 
        return new RefreshTokenTable()
        {
            Id = refreshToken.Id,
            Audience = refreshToken.Audience,
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            UserId = refreshToken.UserId
        };
    }
    
    public static RefreshToken ToDetails(this RefreshTokenTable table)
    { 
        return new RefreshToken()
        {
            Id = table.Id,
            Audience = table.Audience,
            Token = table.Token,
            Expires = table.Expires,
            UserId = table.UserId,
            User = table.User
        };
    }
}
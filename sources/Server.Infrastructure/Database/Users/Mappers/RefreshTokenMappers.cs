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
}
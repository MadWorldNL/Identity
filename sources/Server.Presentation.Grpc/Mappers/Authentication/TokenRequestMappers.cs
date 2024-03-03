using MadWorldNL.Server.Domain.Users.RefreshTokens;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class TokenRequestMappers
{
    public static Token ToToken(this TokenRefreshRequest request)
    {
        return new Token
        {
            RefreshToken = request.RefreshToken,
            Audience = request.Audience
        };
    }
}
using Google.Protobuf.WellKnownTypes;
using MadWorldNL.Server.Domain.Users.Login;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class TokenResponseMappers
{
    public static LoginResponse ToLoginResponse(this TokenResponse response)
    {
        return new LoginResponse
        {
            AccessToken = response.Jwt,
            RefreshToken = response.RefreshToken,
            AccessTokenExpiration = response.JwtExpires.ToTimestamp(),
            IsSuccess = response.IsSuccess,
            RefreshTokenExpiration = response.RefreshTokenExpires.ToTimestamp()
        };
    }
}
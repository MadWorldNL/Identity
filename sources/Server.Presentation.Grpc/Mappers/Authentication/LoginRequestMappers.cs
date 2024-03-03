using MadWorldNL.Server.Domain.Users.Login;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class LoginRequestMappers
{
    public static Login ToLogin(this LoginRequest request)
    {
        return new Login
        {
            Email = request.Username,
            Password = request.Password,
            Audience = request.Audience,
        };
    }
}
using MadWorldNL.Server.Domain.Users.RegisterUsers;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class RegisterRequestMappers
{
    public static NewUser ToNewUser(this RegisterRequest request)
    {
        return new NewUser
        {
            Email = request.Email,
            Password = request.Password,
        };
    }
}
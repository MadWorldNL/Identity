using MadWorldNL.Server.Domain;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class DefaultResponseMappers
{
    public static RegisterResponse ToRegisterResponse(this DefaultResponse response)
    {
        return new RegisterResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
}
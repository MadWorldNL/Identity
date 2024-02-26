using MadWorldNL.Server.Domain;

namespace MadWorldNL.Server.Application.Mappers;

public static class DefaultResultMappers
{
    public static DefaultResponse ToDefaultResponse(this DefaultResult result)
    {
        return new DefaultResponse
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }
}
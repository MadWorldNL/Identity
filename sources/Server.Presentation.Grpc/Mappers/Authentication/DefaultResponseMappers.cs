using MadWorldNL.Server.Domain;
using Server.Presentation.Grpc.Account.V1;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class DefaultResponseMappers
{
    public static ConfirmEmailResponse ToConfirmEmailResponse(this DefaultResponse response)
    {
        return new ConfirmEmailResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
    
    public static ForgetPasswordResponse ToForgotPasswordResponse(this DefaultResponse response)
    {
        return new ForgetPasswordResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
    
    public static RegisterResponse ToRegisterResponse(this DefaultResponse response)
    {
        return new RegisterResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
    
    public static ResendConfirmationEmailResponse ToResendConfirmationEmailResponse(this DefaultResponse response)
    {
        return new ResendConfirmationEmailResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
    
    public static ResetPasswordResponse ToResetPasswordResponse(this DefaultResponse response)
    {
        return new ResetPasswordResponse
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message
        };
    }
}
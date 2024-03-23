using Grpc.Core;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;
using Microsoft.AspNetCore.Authorization;
using Server.Presentation.Grpc.Account.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

[Authorize]
public class AccountService : Account.AccountBase
{
    private readonly ILogger<AccountService> _logger;
    private readonly ConfirmEmailUseCase _confirmEmailUseCase;
    private readonly ForgotPasswordUseCase _forgotPasswordUseCase;
    private readonly ResendConfirmationEmailUseCase _resendConfirmationEmailUseCase;
    private readonly ResetPasswordUseCase _resetPasswordUseCase;

    public AccountService(ILogger<AccountService> logger, 
        ConfirmEmailUseCase confirmEmailUseCase, 
        ForgotPasswordUseCase forgotPasswordUseCase,
        ResendConfirmationEmailUseCase resendConfirmationEmailUseCase,
        ResetPasswordUseCase resetPasswordUseCase)
    {
        _logger = logger;
        _confirmEmailUseCase = confirmEmailUseCase;
        _forgotPasswordUseCase = forgotPasswordUseCase;
        _resendConfirmationEmailUseCase = resendConfirmationEmailUseCase;
        _resetPasswordUseCase = resetPasswordUseCase;
    }

    public override async Task<ConfirmEmailResponse> ConfirmEmail(ConfirmEmailRequest request, ServerCallContext context)
    {
        var response = await _confirmEmailUseCase.ConfirmEmail(request.Email, request.ConfirmCode);
        return response.ToConfirmEmailResponse();
    }

    public override async Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request, ServerCallContext context)
    {
        var response = await _forgotPasswordUseCase.ForgotPassword(request.Email);
        return response.ToForgotPasswordResponse();
    }

    public override async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, ServerCallContext context)
    {
        var response = await _resetPasswordUseCase.ResetPassword(request.Email, request.ResetCode, request.NewPassword);
        return response.ToResetPasswordResponse();
    }

    public override async Task<ResendConfirmationEmailResponse> ResendConfirmationEmail(ResendConfirmationEmailRequest request, ServerCallContext context)
    {
        var response = await _resendConfirmationEmailUseCase.ResendConfirmationEmail(request.Email);
        return response.ToResendConfirmationEmailResponse();
    }
}
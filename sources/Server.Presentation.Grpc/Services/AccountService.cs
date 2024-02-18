using Grpc.Core;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

public class AccountService : Account.AccountBase
{
    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger)
    {
        _logger = logger;
    }

    public override Task<ConfirmEmailResponse> ConfirmEmail(ConfirmEmailRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<ResendConfirmationEmailResponse> ResendConfirmationEmail(ResendConfirmationEmailRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
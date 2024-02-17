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

    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<LoginResponse> TokenRefresh(RefreshRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
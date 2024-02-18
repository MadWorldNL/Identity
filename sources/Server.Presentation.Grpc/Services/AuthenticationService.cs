using Grpc.Core;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(ILogger<AuthenticationService> logger)
    {
        _logger = logger;
    }

    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<LoginResponse> TokenRefresh(TokenRefreshRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
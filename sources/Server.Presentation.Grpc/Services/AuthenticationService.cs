using Grpc.Core;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly RegisterNewUserUseCase _registerNewUserUseCase;

    public AuthenticationService(ILogger<AuthenticationService> logger, RegisterNewUserUseCase registerNewUserUseCase)
    {
        _logger = logger;
        _registerNewUserUseCase = registerNewUserUseCase;
    }

    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var newUser = request.ToNewUser();
        var defaultResponse = await _registerNewUserUseCase.RegisterNewUser(newUser);
        return defaultResponse.ToRegisterResponse();
    }

    public override Task<LoginResponse> TokenRefresh(TokenRefreshRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
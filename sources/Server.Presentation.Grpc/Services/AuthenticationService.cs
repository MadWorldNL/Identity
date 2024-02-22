using Grpc.Core;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly LoginUseCase _loginUseCase;
    private readonly RegisterNewUserUseCase _registerNewUserUseCase;

    public AuthenticationService(ILogger<AuthenticationService> logger, 
        LoginUseCase loginUseCase, 
        RegisterNewUserUseCase registerNewUserUseCase)
    {
        _logger = logger;
        _loginUseCase = loginUseCase;
        _registerNewUserUseCase = registerNewUserUseCase;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var login = request.ToLogin();
        var token = await _loginUseCase.Login(login);
        return token.ToLoginResponse();
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
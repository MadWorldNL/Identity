using Grpc.Core;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly LoginUseCase _loginUseCase;
    private readonly RegisterNewUserUseCase _registerNewUserUseCase;
    private readonly RefreshTokenUseCase _refreshTokenUseCase;

    public AuthenticationService(ILogger<AuthenticationService> logger, 
        LoginUseCase loginUseCase, 
        RegisterNewUserUseCase registerNewUserUseCase,
        RefreshTokenUseCase refreshTokenUseCase)
    {
        _logger = logger;
        _loginUseCase = loginUseCase;
        _registerNewUserUseCase = registerNewUserUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
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

    public override async Task<LoginResponse> TokenRefresh(TokenRefreshRequest request, ServerCallContext context)
    {
        var token = request.ToToken();
        var newToken = await _refreshTokenUseCase.RefreshToken(token);
        return newToken.ToLoginResponse();
    }
}
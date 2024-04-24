using Google.Protobuf.WellKnownTypes;
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Api.Shared.Settings;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Server.Presentation.Grpc.Authentication.V1;
using LoginRequest = Server.Presentation.Grpc.Authentication.V1.LoginRequest;

namespace MadWorldNL.Clients.Identity.Api.Shared.Services;

public sealed class AuthenticationService(
    Authentication.AuthenticationClient client,
    IOptions<IdentitySettings> identitySettings)
{
    private readonly IdentitySettings _identitySettings = identitySettings.Value;

    public async Task<LoginProxyResponse> AuthenticateAsync(LoginProxyRequest proxyRequest)
    {
        var request = new LoginRequest
        {
            Audience = _identitySettings.Audience,
            Username = proxyRequest.Email,
            Password = proxyRequest.Password
        };
        
        var response = await client.LoginAsync(request);

        return new LoginProxyResponse()
        {
            IsSuccess = response.IsSuccess,
            AccessToken = response.AccessToken,
            AccessTokenExpiration = response.AccessTokenExpiration.ToDateTime(),
            RefreshToken = response.RefreshToken,
            RefreshTokenExpiration = response.RefreshTokenExpiration.ToDateTime()
        };
    }

    public async Task<LogoutProxyResponse> LogoutAsync()
    {
        var response = await client.LogoutAsync(new Empty());

        return new LogoutProxyResponse()
        {
            IsSuccess = response.IsSuccess
        };
    }
    
    public async Task<LoginProxyResponse> RefreshTokenAsync(RefreshTokenProxyRequest proxyRequest)
    {
        var request = new TokenRefreshRequest()
        {
            Audience = _identitySettings.Audience,
            RefreshToken = proxyRequest.AccessToken
        };
        
        var response = await client.TokenRefreshAsync(request);

        return new LoginProxyResponse()
        {
            IsSuccess = response.IsSuccess,
            AccessToken = response.AccessToken,
            AccessTokenExpiration = response.AccessTokenExpiration.ToDateTime(),
            RefreshToken = response.RefreshToken,
            RefreshTokenExpiration = response.RefreshTokenExpiration.ToDateTime()
        };
    }
}
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Api.Shared.Settings;
using Microsoft.Extensions.Options;
using Server.Presentation.Grpc.Authentication.V1;

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
            Expiration = response.Expiration.ToDateTime(),
            RefreshToken = response.RefreshToken
        };
    }
}
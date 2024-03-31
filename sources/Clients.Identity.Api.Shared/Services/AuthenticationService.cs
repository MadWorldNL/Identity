using MadWorldNL.Clients.Identity.Api.Shared.Authentications;
using MadWorldNL.Clients.Identity.Api.Shared.Settings;
using Microsoft.Extensions.Options;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Clients.Identity.Api.Shared.Services;

public sealed class AuthenticationService
{
    private readonly Authentication.AuthenticationClient _client;
    private readonly IdentitySettings _identitySettings;

    public AuthenticationService(Authentication.AuthenticationClient client, IOptions<IdentitySettings> identitySettings)
    {
        _client = client;
        _identitySettings = identitySettings.Value;
    }
    
    public async Task<LoginResponse> AuthenticateAsync(LoginProxyRequest proxyRequest)
    {
        var request = new LoginRequest
        {
            Audience = _identitySettings.Audience,
            Username = proxyRequest.Email,
            Password = proxyRequest.Password
        };
        
        return await _client.LoginAsync(request);
    }
}
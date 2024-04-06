using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationManager(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    public async Task<LoginProxyResponse> LoginAsync(LoginProxyRequest request)
    {
        return await _authenticationService.LoginAsync(request);
    }
}
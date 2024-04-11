using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAuthenticationStorage _authenticationStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationManager(IAuthenticationService authenticationService, IAuthenticationStorage authenticationStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationService = authenticationService;
        _authenticationStorage = authenticationStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    public async Task<LoginProxyResponse> LoginAsync(LoginProxyRequest request)
    {
        var response = await _authenticationService.LoginAsync(request);
        await _authenticationStorage.SetAccessTokenAsync(response);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
        
        return response;
    }

    public async Task LogoutAsync()
    {
        await _authenticationStorage.SetAccessTokenAsync(new LoginProxyResponse());
        await _authenticationStateProvider.GetAuthenticationStateAsync();
    }
}
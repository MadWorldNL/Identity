using MadWorldNL.Clients.Admin.Domain;
using MadWorldNL.Clients.Admin.Services.Authentications;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IAuthenticationService _authenticationService;
    private readonly JwtAuthenticationStateProvider _authenticationStateProvider;
    private readonly ProtectedLocalStorage _localStorage;

    public AuthenticationManager(
        IAuthenticationService authenticationService, 
        AuthenticationStateProvider authenticationStateProvider, 
        ProtectedLocalStorage localStorage)
    {
        _authenticationService = authenticationService;
        _authenticationStateProvider = (JwtAuthenticationStateProvider)authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticationToken> LoginFromSessionAsync()
    {
        var storageResult = await _localStorage.GetAsync<AuthenticationToken>(AuthenticationToken.Entry);

        if (!storageResult.Success || !(storageResult.Value?.IsSuccess ?? false))
        {
            return new AuthenticationToken();
        }
        
        _authenticationStateProvider.Authenticate(storageResult.Value.AccessToken);
        return storageResult.Value;

    } 

    public async Task<AuthenticationToken> LoginAsync(string username, string password)
    {
        var token = _authenticationService.Login(username, password, "localhost");

        if (!token.IsSuccess)
        {
            return token;
        }
        
        await _localStorage.SetAsync(AuthenticationToken.Entry, token);
        _authenticationStateProvider.Authenticate(token.AccessToken);

        return token;
    }

    public async Task LogoutAsync()
    {
        _authenticationStateProvider.Logout();
        await _localStorage.DeleteAsync(AuthenticationToken.Entry);
    }
}
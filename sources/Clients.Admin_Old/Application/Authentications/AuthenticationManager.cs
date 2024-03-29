using MadWorldNL.Clients.Admin.Domain.Authentications;
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
    
    public async Task<AuthenticationToken> GetCurrentAuthenticationTokenAsync()
    {
        var storageResult = await _localStorage.GetAsync<AuthenticationToken>(AuthenticationToken.Entry);

        if (!storageResult.Success || !(storageResult.Value?.IsSuccess ?? false))
        {
            return new AuthenticationToken();
        }
        
        return storageResult.Value;
    }

    public async Task<AuthenticationToken> LoginFromSessionAsync()
    {
        var token = await GetCurrentAuthenticationTokenAsync();

        if (token.IsSuccess)
        {
            _authenticationStateProvider.Authenticate(token);    
        }
        
        return token;
    } 

    public async Task<AuthenticationToken> LoginAsync(string username, string password)
    {
        var token = _authenticationService.Login(username, password, "localhost");

        if (!token.IsSuccess)
        {
            return token;
        }
        
        await AuthenticateAsync(token);
        return token;
    }

    public async Task LogoutAsync()
    {
        _authenticationStateProvider.Logout();
        await _localStorage.DeleteAsync(AuthenticationToken.Entry);
    }
    
    public async Task<AuthenticationToken> RefreshTokenAsync()
    {
        var token = await GetCurrentAuthenticationTokenAsync();

        if (!token.IsSuccess)
        {
            return token;
        }
        
        token = _authenticationService.RefreshToken(token.RefreshToken);

        if (!token.IsSuccess)
        {
            await LogoutAsync();
            return token;
        }

        await AuthenticateAsync(token);
        return token;
    }

    private async Task AuthenticateAsync(AuthenticationToken token)
    {
        await _localStorage.SetAsync(AuthenticationToken.Entry, token);
        _authenticationStateProvider.Authenticate(token); 
    }
}
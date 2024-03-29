using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MadWorldNL.Clients.Admin.Domain.Authentications;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "jwt";

    private AuthenticationToken _currentToken = new();
    private AuthenticationState _currentUserState = GetAnonymous();
    
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(_currentUserState);
    }
    
    public AuthenticationToken GetCurrentToken()
    {
        return _currentToken;
    }

    public void Authenticate(AuthenticationToken token)
    {
        _currentToken = token;
        
        var securityToken = new JwtSecurityToken(token.AccessToken);
        var identity = new ClaimsIdentity(securityToken.Claims, AuthenticationType, "name", "role");
        var user = new ClaimsPrincipal(identity);
        _currentUserState = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(_currentUserState));
    }

    public void Logout()
    {
        _currentToken = new AuthenticationToken();
        
        _currentUserState = GetAnonymous();
        NotifyAuthenticationStateChanged(Task.FromResult(_currentUserState));
    }

    private static AuthenticationState GetAnonymous()
    {
        var user = new ClaimsPrincipal();
        return new AuthenticationState(user);
    }
}
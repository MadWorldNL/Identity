using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "jwt";

    private AuthenticationState _currentUserState = GetAnonymous();
    
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(_currentUserState);
    }

    public void Authenticate(string jwtToken)
    {
        var securityToken = new JwtSecurityToken(jwtToken);
        var identity = new ClaimsIdentity(securityToken.Claims, AuthenticationType);
        var user = new ClaimsPrincipal(identity);
        _currentUserState = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(_currentUserState));
    }

    public void Logout()
    {
        _currentUserState = GetAnonymous();
        NotifyAuthenticationStateChanged(Task.FromResult(_currentUserState));
    }

    private static AuthenticationState GetAnonymous()
    {
        var user = new ClaimsPrincipal();
        return new AuthenticationState(user);
    }
}
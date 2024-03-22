using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "jwt";
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }

    public void Authenticate(string jwtToken)
    {
        var securityToken = new JwtSecurityToken(jwtToken);
        var identity = new ClaimsIdentity(securityToken.Claims, AuthenticationType);
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }
}
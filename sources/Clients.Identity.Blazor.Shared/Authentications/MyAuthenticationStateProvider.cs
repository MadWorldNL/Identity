using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthenticationStorage _authenticationStorage;

    public MyAuthenticationStateProvider(IAuthenticationStorage authenticationStorage)
    {
        _authenticationStorage = authenticationStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _authenticationStorage.GetAccessTokenAsync();

        var identity = new ClaimsIdentity();
        if (accessToken.IsSuccess)
        {
            identity = RetrieveUserFromJwt(accessToken.AccessToken);
        }
        
        var state = new AuthenticationState(new ClaimsPrincipal(identity));
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }
    
    private static ClaimsIdentity RetrieveUserFromJwt(string jwt)
    {
        var claims = ParseClaimsFromJwt(jwt).ToList();
        return new ClaimsIdentity(claims, "jwt", "nameid", "role");
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
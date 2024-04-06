using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var state = new AuthenticationState(new ClaimsPrincipal());
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return Task.FromResult(state);
    }
}
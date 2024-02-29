using System.Security.Claims;
using MadWorldNL.Server.Domain.Authorizations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;


public class FakePolicyEvaluator : IPolicyEvaluator
{
    public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var principal = new ClaimsPrincipal();
        
        principal.AddIdentity(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, Roles.IdentityAdminstrator),
            new Claim(ClaimTypes.NameIdentifier, "FakeAccount")
        },"FakeScheme"));

        return await Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(principal, 
                new AuthenticationProperties(), "FakeScheme")));
    }

    public async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
        object? resource)
    {
        return await Task.FromResult(PolicyAuthorizationResult.Success());
    }
}
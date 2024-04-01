using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Api.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadWorldNL.Clients.Identity.Api.Shared.Endpoints;

public static class EndpointsExtensions
{
    public static void AddIdentityEndpoints(this WebApplication app)
    {
        app.AddAuthenticationEndpoints();
    }

    private static void AddAuthenticationEndpoints(this WebApplication app)
    {
        var authenticationEndpoints = app.MapGroup("/Authentication")
            .WithTags("Authentication");

        authenticationEndpoints.MapPost("/Login",
                ([FromBody] LoginProxyRequest request, [FromServices] AuthenticationService authenticationService) =>
                    authenticationService.AuthenticateAsync(request))
            .WithName("Login");
    }
}
using MadWorldNL.Clients.Identity.Api.Shared.Authentications;
using MadWorldNL.Clients.Identity.Api.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Presentation.Grpc.Authentication.V1;

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
                ([AsParameters] LoginProxyRequest request, [FromServices] AuthenticationService authenticationService) =>
                    authenticationService.AuthenticateAsync(request))
            .WithName("Login");
    }
}
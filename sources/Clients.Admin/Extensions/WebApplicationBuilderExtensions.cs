using MadWorldNL.Clients.Admin.Application.Authentications;
using MadWorldNL.Clients.Admin.Services.Authentications;
using Microsoft.AspNetCore.Components.Authorization;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Clients.Admin.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddGrpcClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<Authentication.AuthenticationClient>(o =>
        {
            o.Address = new Uri(builder.Configuration["Identity:Host"]!);
        });
    }

    public static void AddAdminServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        
        builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
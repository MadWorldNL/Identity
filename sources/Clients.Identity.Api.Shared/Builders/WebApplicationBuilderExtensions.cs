using MadWorldNL.Clients.Identity.Api.Shared.Services;
using MadWorldNL.Clients.Identity.Api.Shared.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Clients.Identity.Api.Shared.Builders;

public static class WebApplicationBuilderExtensions
{
    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        var identitySettings = builder.Configuration.GetSection(IdentitySettings.Entry);
        
        builder.Services.AddOptions<IdentitySettings>()
            .Bind(identitySettings)
            .ValidateDataAnnotations();
        
        builder.Services.AddGrpcClient<Authentication.AuthenticationClient>(option =>
        {
            var identityHost = identitySettings.GetValue<string>(nameof(IdentitySettings.Host))!;
            option.Address = new Uri(identityHost);
        });

        builder.Services.AddScoped<AuthenticationService>();
    }
}
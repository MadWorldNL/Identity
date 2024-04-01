using MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;
using MadWorldNL.Clients.Identity.Blazor.Shared.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddIdentity(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOptions<IdentitySettings>()
            .Bind(builder.Configuration.GetSection(IdentitySettings.Entry));

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        builder.AddIdentityClients();
    }

    private static void AddIdentityClients(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient(ApiTypes.AnonymousIdentity, (serviceProvider, client) =>
        {
            var identitySettingsOption = serviceProvider.GetService<IOptions<IdentitySettings>>()!;
            client.BaseAddress = new Uri(identitySettingsOption.Value.Host!);
        });
        
        builder.Services.AddHttpClient(ApiTypes.Identity, (serviceProvider, client) =>
        {
            var identitySettingsOption = serviceProvider.GetService<IOptions<IdentitySettings>>()!;
            client.BaseAddress = new Uri(identitySettingsOption.Value.Host!);
        });
    }
}
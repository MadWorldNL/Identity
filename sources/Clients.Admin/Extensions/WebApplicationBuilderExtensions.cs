using MadWorldNL.Clients.Admin.Services;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Clients.Admin.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddGrpcClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<Authentication.AuthenticationClient>(o =>
        {
            o.Address = new Uri("https://localhost:5001"); // Replace with your gRPC service's address
        });
    }

    public static void AddAdminServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
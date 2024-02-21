using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database.Users;

namespace MadWorldNL.Server.Presentation.Grpc.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIdentityMadWorldNL(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<RegisterNewUserUseCase>();
        builder.Services.AddScoped<IUserManager, UserManager>();
    }
}
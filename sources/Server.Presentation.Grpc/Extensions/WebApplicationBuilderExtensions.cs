using MadWorldNL.Server.Application.Jwt;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Application.UserSettings;
using MadWorldNL.Server.Domain.Jwt;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database.Users;

namespace MadWorldNL.Server.Presentation.Grpc.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIdentityMadWorldNL(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        builder.Services.AddScoped<ConfirmEmailUseCase>();
        builder.Services.AddScoped<ForgotPasswordUseCase>();
        builder.Services.AddScoped<GetAllRolesUseCase>();
        builder.Services.AddScoped<LoginUseCase>();
        builder.Services.AddScoped<RefreshTokenUseCase>();
        builder.Services.AddScoped<RegisterNewUserUseCase>();
        builder.Services.AddScoped<ResendConfirmationEmailUseCase>();
        builder.Services.AddScoped<ResetPasswordUseCase>();
        builder.Services.AddScoped<IUserManager, UserManager>();
        builder.Services.AddScoped<ISignInManager, SignInManager>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
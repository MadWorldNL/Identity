using System.Text;
using Common.Grpc;
using MadWorldNL.Common.AspNetCore;
using MadWorldNL.Server.Domain.Authorizations;
using MadWorldNL.Server.Domain.Jwt;
using MadWorldNL.Server.Infrastructure.Database;
using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.Extensions;
using MadWorldNL.Server.Presentation.Grpc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(nameof(JwtSettings))
);

// Add services to the container.
builder.Services.AddGrpc(grpc =>
{
    grpc.Interceptors.Add<GrpcGlobalErrorHandlerInterceptor>();
});

builder.Services.AddGrpcReflection();

builder.AddCommonMadWorldNL();
builder.AddIdentityMadWorldNL();

builder.Services.AddDbContext<UserDbContext>(
    options =>
        options.UseNpgsql(builder.BuildConnectionString("IdentityConnectionString")));

builder.Services.AddIdentity<IdentityUserExtended, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUserExtended>>(TokenOptions.DefaultProvider);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
                .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.RequireIdentityAdministratorRole,
        policy => policy.RequireRole(Roles.IdentityAdminstrator));
});

var app = builder.Build();
var environment = app.Environment;

app.UseRouting();

// Configure the HTTP request pipeline.
app.MapGrpcService<AccountService>();
app.MapGrpcService<AuthenticationService>();
app.MapGrpcService<UserManagerService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

if (environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseAuthentication();
app.UseAuthorization();

app.MigrateDatabase<UserDbContext>();
await app.AddAllIdentityRoles();
await app.AddFirstAdminAccount();

app.Run();

public partial class Program { }
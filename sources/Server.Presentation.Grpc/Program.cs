using System.Text;
using MadWorldNL.Common.AspNetCore;
using MadWorldNL.Server.Infrastructure.Database;
using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.Extensions;
using MadWorldNL.Server.Presentation.Grpc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.AddCommonMadWorldNL();
builder.AddIdentityMadWorldNL();

builder.Services.AddDbContext<UserDbContext>(
    options =>
        options.UseNpgsql(builder.BuildConnectionString("IdentityConnectionString")));

builder.Services.AddIdentity<IdentityUserExtended, IdentityRole>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AccountService>();
app.MapGrpcService<AuthenticationService>();
app.MapGrpcService<UserManagerService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseAuthentication();
app.UseAuthorization();

app.MigrateDatabase<UserDbContext>();

app.Run();
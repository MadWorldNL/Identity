using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database;
using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Authentication.V1;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AuthenticationServices;

[Collection(Collections.Applcation)]
public class TokenRefresh : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public TokenRefresh(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Login_GivenCredentials_ReturnsToken()
    {
        // Arrange
        const string audience = "identity@website.nl";
        const string email = "test@test.nl";
        const string password = "Test1234!";
        const string token = "1234567890";
        
        using var scope = _factory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>();
        var user = new IdentityUserExtended()
        {
            UserName = email,
            Email = email
        };
        await userManager.CreateAsync(user);
        await userManager.AddPasswordAsync(user, password);

        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        var refreshToken = new RefreshTokenTable()
        {
            Audience = audience,
            Token = token,
            UserId = user.Id,
            Expires = new DateTime(2000, 2, 1, 0, 0, 0, DateTimeKind.Utc)
        };
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();
        
        var request = new TokenRefreshRequest()
        {
            Audience = audience,
            RefreshToken = token
        };
        
        var authenticationClient = new Authentication.AuthenticationClient(_factory.Channel);
        
        // Act
        var response = authenticationClient.TokenRefresh(request);

        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.AccessToken.ShouldNotBeEmpty();
        response.RefreshToken.ShouldBe(token);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
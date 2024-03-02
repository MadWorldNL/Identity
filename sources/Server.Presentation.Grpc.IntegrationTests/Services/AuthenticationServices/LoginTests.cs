using MadWorldNL.Server.Infrastructure.Database;
using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AuthenticationServices;

[Collection(Collections.Applcation)]
public class LoginTests : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public LoginTests(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Register_GivenCredentials_ReturnsToken()
    {
        // Arrange
        const string email = "test@test.nl";
        const string password = "Test1234!";
        
        using var scope = _factory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>();
        var user = new IdentityUserExtended()
        {
            UserName = email,
            Email = email
        };
        await userManager.CreateAsync(user);
        await userManager.AddPasswordAsync(user, password);

        var request = new LoginRequest()
        {
            Audience = "identity@website.nl",
            Username = email,
            Password = password
        };
        
        var authenticationClient = new Authentication.AuthenticationClient(_factory.Channel);
        
        // Act
        var response = authenticationClient.Login(request);

        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.AccessToken.ShouldNotBeEmpty();
        response.RefreshToken.ShouldNotBeEmpty();
        
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        var refreshTokens = context.RefreshTokens.AsNoTracking()
            .Include(r => r.User)
            .ToList();
        
        refreshTokens.Count.ShouldBe(1);
        refreshTokens[0].User.Email.ShouldBe(email);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
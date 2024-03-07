using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Account.V1;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AccountServices;

[Collection(Collections.Applcation)]
public class ResetPasswordTests : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public ResetPasswordTests(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ResetPassword_GivenResetCode_ReturnsOK()
    {
        // Arrange
        const string email = "test@test.nl";
        const string newPassword = "Test1234!";

        string token;
        using (var scope = _factory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>(); 
            
            var user = new IdentityUserExtended()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
        
            await userManager.CreateAsync(user);
            token = await userManager.GeneratePasswordResetTokenAsync(user);
        }
        
        var request = new ResetPasswordRequest()
        {
            ResetCode = token,
            Email = email,
            NewPassword = newPassword
        };
        
        var client = new Account.AccountClient(_factory.Channel);
        
        // Act
        var response = client.ResetPassword(request);
        
        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.Message.ShouldBe(string.Empty);
        
        using var assertScope = _factory.CreateScope();
        var assertUserManager = assertScope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>();
        var assertUser = assertUserManager.Users.First();
        assertUser.PasswordHash.ShouldNotBeEmpty();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
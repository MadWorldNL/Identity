using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Account.V1;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AccountServices;

[Collection(Collections.Applcation)]
public class ConfirmEmailTests : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public ConfirmEmailTests(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ConfirmEmail_GivenToken_ReturnsOK()
    {
        // Arrange
        const string email = "test@test.nl";

        string token;
        using (var scope = _factory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>(); 
            
            var user = new IdentityUserExtended()
            {
                UserName = email,
                Email = email
            };
        
            await userManager.CreateAsync(user);
            token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            
        }

        var request = new ConfirmEmailRequest()
        {
            ConfirmCode = token,
            Email = email
        };
        
        var client = new Account.AccountClient(_factory.Channel);

        // Act
        var response = client.ConfirmEmail(request);

        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.Message.ShouldBe(string.Empty);

        using var assertScope = _factory.CreateScope();
        var assertUserManager = assertScope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>(); 
        var assertUser = await assertUserManager.FindByEmailAsync(email);
        assertUser.ShouldNotBeNull();
        assertUser.EmailConfirmed.ShouldBeTrue();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
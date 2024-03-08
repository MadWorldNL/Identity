using MadWorldNL.Server.Infrastructure.Database.Users;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Account.V1;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AccountServices;

[Collection(Collections.Applcation)]
public class ForgetPasswordTests : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public ForgetPasswordTests(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ForgetPassword_GivenExistingEmail_ReturnsOK()
    {
        // Arrange
        const string email = "test@test.nl";
        
        using (var scope = _factory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserExtended>>(); 
            
            var user = new IdentityUserExtended()
            {
                UserName = email,
                Email = email
            };
        
            await userManager.CreateAsync(user);
        }
        
        var request = new ForgetPasswordRequest()
        {
            Email = email
        };
        
        var client = new Account.AccountClient(_factory.Channel);
        
        // Act
        var response = client.ForgetPassword(request);
        
        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.Message.ShouldNotBeNullOrEmpty();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
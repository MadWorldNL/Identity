using MadWorldNL.Server.Infrastructure.Database;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Presentation.Grpc.Authentication.V1;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.AuthenticationServices;

[Collection(Collections.Applcation)]
public class RegisterTests : IAsyncLifetime
{
    private readonly GrpcFactory _factory;

    public RegisterTests(GrpcFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Register_GivenUser_ReturnsOK()
    {
        // Arrange
        var registerRequest = new RegisterRequest()
        {
            Email = "test@test.nl",
            Password = "Test1234!"
        };
        
        var authenticationClient = new Authentication.AuthenticationClient(_factory.Channel);
        
        // Act
        var response = authenticationClient.Register(registerRequest);
        
        // Assert
        response.IsSuccess.ShouldBeTrue();
        response.Message.ShouldBe("");
        
        using var scope = _factory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        
        var users = context.Users.AsNoTracking().ToList();
        users.Count.ShouldBe(1);
        users[0].Email.ShouldBe(registerRequest.Email);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync()
    {
        return _factory.ResetDatabase();
    }
}
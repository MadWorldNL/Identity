using MadWorldNL.Server.Domain.Authorizations;
using MadWorldNL.Server.Presentation.Grpc.IntegrationTests.TestBase;
using MadWorldNL.Server.Presentation.Grpc.Services;
using Server.Presentation.Grpc;
using Shouldly;

namespace MadWorldNL.Server.Presentation.Grpc.IntegrationTests.Services.UserManagerServices;

[Collection(Collections.Applcation)]
public class GetRolesTests
{
    private readonly GrpcFactory _factory;

    public GetRolesTests(GrpcFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetRoles_WhenCalled_ReturnsRoles()
    {
        // Arrange
        var userManagerClient = new UserManager.UserManagerClient(_factory.Channel);
        
        // Act
        var response = userManagerClient.GetRoles(new GetRolesRequest());

        // Assert
        response.Roles.Count.ShouldBe(1);
        response.Roles[0].ShouldBe(Roles.IdentityAdminstrator);
    }
}
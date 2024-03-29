using Server.Presentation.Grpc.UserManager.V1;

namespace MadWorldNL.Clients.Admin.Services.Authentications;

public class UserManagerService : IUserManagerService
{
    private readonly UserManager.UserManagerClient _client;

    public UserManagerService(UserManager.UserManagerClient client)
    {
        _client = client;
    }

    public IReadOnlyList<User> GetUsers(int page)
    {
        var response = _client.GetUsers(new GetUsersRequest()
        {
            Page = page
        });

        return response.Users;
    }
}
using Server.Presentation.Grpc.UserManager.V1;

namespace MadWorldNL.Clients.Admin.Services.Authentications;

public interface IUserManagerService
{
    IReadOnlyList<User> GetUsers(int page);
}
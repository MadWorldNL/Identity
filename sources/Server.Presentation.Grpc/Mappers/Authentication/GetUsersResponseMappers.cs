using MadWorldNL.Server.Domain.Users;
using Server.Presentation.Grpc.UserManager.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;

public static class GetUsersResponseMappers
{
    public static GetUsersResponse ToGetUsersResponse(this IReadOnlyList<IIdentityUser> users)
    {
        return new GetUsersResponse()
        {
            Users = { users.Select(ToUser) }
        };
    }
    
    private static User ToUser(this IIdentityUser user)
    {
        return new User()
        {
            Id = user.Id,
            Email = user.Email,
            IsBlocked = user.LockoutEnabled
        };
    }
}
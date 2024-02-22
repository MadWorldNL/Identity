using MadWorldNL.Server.Domain.Users.RegisterUsers;

namespace MadWorldNL.Server.Domain.Users;

public interface IUserManager
{
    Task<DefaultResponse> CreateAsync(NewUser user);
    Task<IIdentityUser> FindByEmailAsync(string email);
    Task<IList<string>> GetRolesByEmailAsync(string email);
}
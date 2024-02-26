using MadWorldNL.Server.Domain.Users.ForgotPasswords;
using MadWorldNL.Server.Domain.Users.RegisterUsers;

namespace MadWorldNL.Server.Domain.Users;

public interface IUserManager
{
    Task<DefaultResult> CreateAsync(NewUser user);
    Task<DefaultResult> ConfirmEmailAsync(string email, string token);
    Task<IIdentityUser> FindByEmailAsync(string email);
    Task<ForgotPasswordResult> ForgotPasswordAsync(string email);
    Task<IList<string>> GetRolesByEmailAsync(string email);
    Task<DefaultResult> ResetPasswordAsync(string email, string token, string newPassword);
}
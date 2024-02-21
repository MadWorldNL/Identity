using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Domain.Users.RegisterUsers;

namespace MadWorldNL.Server.Application.Users;

public class RegisterNewUserUseCase
{
    private readonly IUserManager _userManager;

    public RegisterNewUserUseCase(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<DefaultResponse> RegisterNewUser(NewUser user)
    {
        return await _userManager.CreateAsync(user);
    }
}
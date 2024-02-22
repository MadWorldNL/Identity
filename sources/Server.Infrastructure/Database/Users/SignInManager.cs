using MadWorldNL.Server.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public class SignInManager : ISignInManager
{
    private readonly SignInManager<IdentityUserExtended> _identitySignInManager;

    public SignInManager(SignInManager<IdentityUserExtended> identitySignInManager)
    {
        _identitySignInManager = identitySignInManager;
    }

    public async Task<bool> CanUserSignIn(string email, string password)
    {
        var result = await _identitySignInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}
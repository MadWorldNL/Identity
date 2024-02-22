namespace MadWorldNL.Server.Domain.Users;

public interface ISignInManager
{
    Task<bool> CanUserSignIn(string email, string password);
}
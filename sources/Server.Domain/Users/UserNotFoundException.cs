namespace MadWorldNL.Server.Domain.Users;

public class UserNotFoundException : Exception
{
    public readonly string Email;

    public UserNotFoundException(string email)
    {
        Email = email;
    }
}
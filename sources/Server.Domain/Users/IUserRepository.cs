namespace MadWorldNL.Server.Domain.Users;

public interface IUserRepository
{
    Task AddRefreshToken(RefreshToken token);
}
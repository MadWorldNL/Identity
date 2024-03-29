namespace MadWorldNL.Server.Domain.Users;

public interface IUserRepository
{
    Task AddRefreshToken(RefreshToken token);
    Task<RefreshToken?> GetRefreshToken(string token);
    Task<List<string>> GetRoles();
    IReadOnlyList<IIdentityUser> GetUsers(int page);
}
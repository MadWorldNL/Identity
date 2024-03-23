using MadWorldNL.Clients.Admin.Domain.Authentications;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public interface IAuthenticationManager
{
    Task<AuthenticationToken> GetCurrentAuthenticationTokenAsync();
    Task<AuthenticationToken> LoginFromSessionAsync();
    Task<AuthenticationToken> LoginAsync(string username, string password);

    Task LogoutAsync();
    Task<AuthenticationToken> RefreshTokenAsync();
}
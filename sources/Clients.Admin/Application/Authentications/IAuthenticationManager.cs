using MadWorldNL.Clients.Admin.Domain;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public interface IAuthenticationManager
{
    Task<AuthenticationToken> LoginFromSessionAsync();
    Task<AuthenticationToken> LoginAsync(string username, string password);

    Task LogoutAsync();
}
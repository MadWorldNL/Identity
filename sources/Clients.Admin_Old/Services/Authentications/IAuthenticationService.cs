using MadWorldNL.Clients.Admin.Domain.Authentications;

namespace MadWorldNL.Clients.Admin.Services.Authentications;

public interface IAuthenticationService
{
    AuthenticationToken Login(string username, string password, string audience);
    AuthenticationToken RefreshToken(string refreshToken);
}
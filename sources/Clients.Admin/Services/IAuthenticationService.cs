using MadWorldNL.Clients.Admin.Domain;

namespace MadWorldNL.Clients.Admin.Services;

public interface IAuthenticationService
{
    AuthenticationToken Login(string username, string password, string audience);
}
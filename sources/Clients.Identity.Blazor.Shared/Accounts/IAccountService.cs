using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Accounts;

public interface IAccountService
{
    Task<LogoutProxyResponse> LogoutAsync();
}
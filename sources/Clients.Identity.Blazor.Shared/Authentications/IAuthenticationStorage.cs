using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public interface IAuthenticationStorage
{
    Task SetAccessTokenAsync(LoginProxyResponse response);
    Task<LoginProxyResponse> GetAccessTokenAsync();
}
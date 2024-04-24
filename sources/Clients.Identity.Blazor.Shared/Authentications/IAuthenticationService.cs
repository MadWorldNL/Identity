using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public interface IAuthenticationService
{
    Task<LoginProxyResponse> LoginAsync(LoginProxyRequest request);
    Task<LoginProxyResponse> RefreshTokenAsync(RefreshTokenProxyRequest request);
}
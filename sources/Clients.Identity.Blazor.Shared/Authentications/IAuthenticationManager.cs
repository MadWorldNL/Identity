using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public interface IAuthenticationManager
{
    Task<LoginProxyResponse> LoginAsync(LoginProxyRequest request);
    Task<LoginProxyResponse> RefreshAsync(string accessToken);
    Task<LoginProxyResponse> GetActiveTokenFromSession();
}
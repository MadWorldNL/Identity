using System.Net.Http.Json;
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Blazor.Shared.Settings;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class AuthenticationService : IAuthenticationService
{
    private const string Endpoint = "Authentication";
    
    private readonly HttpClient _client;
    public AuthenticationService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.AnonymousIdentity);
    }
    
    public async Task<LoginProxyResponse> LoginAsync(LoginProxyRequest request)
    {
        var response = await _client.PostAsJsonAsync($"{Endpoint}/Login", request);
        return await response.Content.ReadFromJsonAsync<LoginProxyResponse>() ?? new LoginProxyResponse();
    }
}
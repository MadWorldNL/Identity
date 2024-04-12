using System.Net.Http.Json;
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Blazor.Shared.Settings;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Accounts;

public class AccountService : IAccountService
{
    private const string Endpoint = "Authentication";
    
    private readonly HttpClient _httpClient;
    
    public AccountService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(ApiTypes.Identity);
    }
    
    public async Task<LogoutProxyResponse> LogoutAsync()
    {
        return await _httpClient.GetFromJsonAsync<LogoutProxyResponse>($"{Endpoint}/Logout") 
               ?? new LogoutProxyResponse();
    }
}
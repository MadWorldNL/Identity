using Blazored.LocalStorage;
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class AuthenticationLocalStorage : IAuthenticationStorage
{
    private const string AuthenticationResponseName = "AuthenticationResponse";
    
    private readonly ILocalStorageService _localStorage;

    public AuthenticationLocalStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SetAccessTokenAsync(LoginProxyResponse response)
    {
        await _localStorage.SetItemAsync(AuthenticationResponseName, response);
    }

    public async Task<LoginProxyResponse> GetAccessTokenAsync()
    {
        return await _localStorage.GetItemAsync<LoginProxyResponse>(AuthenticationResponseName) 
               ?? new LoginProxyResponse();
    }
}
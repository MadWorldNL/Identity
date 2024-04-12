using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Accounts;

public class AccountManager : IAccountManager
{
    private readonly IAccountService _accountService;
    private readonly IAuthenticationStorage _authenticationStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AccountManager(
        IAccountService accountService,
        IAuthenticationStorage authenticationStorage, 
        AuthenticationStateProvider authenticationStateProvider)
    {
        _accountService = accountService;
        _authenticationStorage = authenticationStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    public async Task LogoutAsync()
    {
        await _accountService.LogoutAsync();
        await _authenticationStorage.SetAccessTokenAsync(new LoginProxyResponse());
        await _authenticationStateProvider.GetAuthenticationStateAsync();
    }
}
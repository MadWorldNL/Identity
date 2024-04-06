using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;
using MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;
using Microsoft.AspNetCore.Components;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Pages;

public partial class Login
{
    [Inject] public IAuthenticationManager AuthenticationManager { get; set; } = null!;

    private LoginProxyRequest _loginRequest = new();
    private bool _showError = false;
    
    private string TempLoginMessage = string.Empty;
    
    public async Task LoginAsync()
    {
        var response = await AuthenticationManager.LoginAsync(_loginRequest);
        
        if (!response.IsSuccess)
        {
            _showError = true;

            return;
        }
        
        _showError = false;
        
        TempLoginMessage = response.Expiration.ToString();
    }
}
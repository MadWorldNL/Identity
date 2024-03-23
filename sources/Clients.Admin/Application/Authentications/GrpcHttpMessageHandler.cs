using MadWorldNL.Clients.Admin.Domain.Authentications;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class GrpcHttpMessageHandler : DelegatingHandler
{
    private readonly IAuthenticationManager _authenticationManager;

    public GrpcHttpMessageHandler(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetJwtTokenAsync();
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        return await base.SendAsync(request, cancellationToken);
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return SendAsync(request, cancellationToken).GetAwaiter().GetResult();
    }

    private async Task<string> GetJwtTokenAsync()
    {
        var authenticationToken = await _authenticationManager.GetCurrentAuthenticationTokenAsync();
        
        if (!authenticationToken.IsSuccess)
        {
            throw new AuthenticationTokenException();
        }

        if (authenticationToken.Expires >= DateTime.UtcNow.AddMinutes(5))
        {
            return authenticationToken.AccessToken;
        }
        
        authenticationToken = await _authenticationManager.RefreshTokenAsync();
            
        if (!authenticationToken.IsSuccess)
        {
            throw new AuthenticationTokenException();
        }

        return authenticationToken.AccessToken;
    }
}
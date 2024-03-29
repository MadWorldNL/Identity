using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorldNL.Clients.Admin.Application.Authentications;

public class GrpcHttpMessageHandler : DelegatingHandler
{
    private readonly UserService _userService;
    private readonly JwtAuthenticationStateProvider _authenticationStateProvider;

    public GrpcHttpMessageHandler(AuthenticationStateProvider authenticationStateProvider, UserService userService)
    {
        _userService = userService;
        _authenticationStateProvider = (JwtAuthenticationStateProvider)authenticationStateProvider;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var test = _userService.GetUser();
        
        var token = _authenticationStateProvider.GetCurrentToken().AccessToken;
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        return await base.SendAsync(request, cancellationToken);
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return SendAsync(request, cancellationToken).GetAwaiter().GetResult();
    }
}
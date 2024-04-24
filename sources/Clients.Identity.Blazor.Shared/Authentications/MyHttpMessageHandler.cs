using System.Net;
using System.Net.Http.Headers;
using MadWorldNL.Clients.Identity.Api.Contracts.Authentications;

namespace MadWorldNL.Clients.Identity.Blazor.Shared.Authentications;

public class MyHttpMessageHandler : DelegatingHandler
{
    private readonly IAuthenticationManager _authenticationManager;

    public MyHttpMessageHandler(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            await AddAuthorizationHeader(request);
            return await base.SendAsync(request, cancellationToken);
        }
        catch (RefreshTokenInvalidException)
        {
            return CreateExceptionMessage();
        }
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            AddAuthorizationHeader(request).Wait(cancellationToken);
            return base.Send(request, cancellationToken);
        }
        catch (RefreshTokenInvalidException)
        {
            return CreateExceptionMessage();
        }
    }

    private async Task AddAuthorizationHeader(HttpRequestMessage request)
    {
        var accessToken = await _authenticationManager.GetActiveTokenFromSession();

        if (!accessToken.IsSuccess)
        {
            throw new RefreshTokenInvalidException();
        }

        if (accessToken.AccessTokenExpiration.AddMinutes(-5) < DateTimeOffset.UtcNow)
        {
            accessToken = await RefreshAccessToken(accessToken);
        }
        
        AddBearerToken(request, accessToken.AccessToken);
    }

    private async Task<LoginProxyResponse> RefreshAccessToken(LoginProxyResponse accessToken)
    {
        return await _authenticationManager.RefreshAsync(accessToken.RefreshToken);
    }
    
    private static void AddBearerToken(HttpRequestMessage request, string token)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    private static HttpResponseMessage CreateExceptionMessage()
    {
        return new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Unauthorized,
        };
    }
}
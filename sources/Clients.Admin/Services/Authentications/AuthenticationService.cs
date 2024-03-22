using MadWorldNL.Clients.Admin.Domain;
using Server.Presentation.Grpc.Authentication.V1;

namespace MadWorldNL.Clients.Admin.Services.Authentications;

public class AuthenticationService : IAuthenticationService
{
    private readonly Authentication.AuthenticationClient _client;

    public AuthenticationService(Authentication.AuthenticationClient client)
    {
        _client = client;
    }

    public AuthenticationToken Login(string username, string password, string audience)
    {
        var request = new LoginRequest()
        {
            Username = username,
            Password = password,
            Audience = audience
        };
        
        var response = _client.Login(request);

        return new AuthenticationToken()
        {
            IsSuccess = response.IsSuccess,
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            Expires = response.Expiration.ToDateTime()
        };
    }
}
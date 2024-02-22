using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Domain.Jwt;

public interface IJwtGenerator
{
    JwtToken GenerateToken(IIdentityUser user, string audience, IList<string> roles);
}
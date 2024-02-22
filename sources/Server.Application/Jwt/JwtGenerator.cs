using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MadWorldNL.Server.Domain.Jwt;
using MadWorldNL.Server.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MadWorldNL.Server.Application.Jwt;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public JwtToken GenerateToken(IIdentityUser user, string audience, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user!.Id),
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
        };

        if (roles.Any())
        {
            claims.AddRange(
                roles.Select(role =>
                    new Claim(ClaimTypes.Role, role)));
        }

        var expires = DateTime.UtcNow.AddMinutes(15);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = _jwtSettings.Issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token)!;

        return new JwtToken()
        {
            Token = jwt,
            Expires = expires
        };
    }
}
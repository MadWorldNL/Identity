using MadWorldNL.Common.Time;
using MadWorldNL.Server.Domain.Jwt;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Domain.Users.Login;
using MadWorldNL.Server.Domain.Users.RefreshTokens;

namespace MadWorldNL.Server.Application.Users;

public class RefreshTokenUseCase
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUserManager _userManager;
    private readonly IJwtGenerator _jwtGenerator;

    public RefreshTokenUseCase(IDateTimeProvider dateTimeProvider, IUserRepository userRepository, IUserManager userManager, IJwtGenerator jwtGenerator)
    {
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }
    
    public async Task<TokenResponse> RefreshToken(Token request)
    {
        var refreshToken = await _userRepository.GetRefreshToken(request.RefreshToken);

        if (refreshToken == null || refreshToken.Expires < _dateTimeProvider.UtcNow())
        {
            return TokenResponse.AccessDenied();
        }

        var user = refreshToken.User;
        var roles = await _userManager.GetRolesByEmailAsync(user.Email!);
        
        var jwt = _jwtGenerator.GenerateToken(user, request.Audience, roles);
        return new TokenResponse()
        {
            IsSuccess = true,
            Jwt = jwt.Token,
            JwtExpires = jwt.Expires,
            RefreshToken = request.RefreshToken,
            RefreshTokenExpires = refreshToken.Expires
        };
    }
}
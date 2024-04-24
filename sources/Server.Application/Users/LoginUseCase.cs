using System.Security.Cryptography;
using MadWorldNL.Server.Domain.Jwt;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Domain.Users.Login;

namespace MadWorldNL.Server.Application.Users;

public class LoginUseCase
{
    private readonly IUserManager _userManager;
    private readonly ISignInManager _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginUseCase(
        IUserManager userManager,
        ISignInManager signInManager,
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }
    
    public async Task<TokenResponse> Login(Login request)
    {
        var isLoginSucceed = await _signInManager.CanUserSignIn(request.Email, request.Password);
        if (!isLoginSucceed)
        {
            return TokenResponse.AccessDenied();
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        var roles = await _userManager.GetRolesByEmailAsync(request.Email);
        
        var jwt = _jwtGenerator.GenerateToken(user!, request.Audience, roles);
        var token = GenerateRefreshToken();

        var refreshTokenExpires = DateTime.UtcNow.AddDays(7);
        var refreshToken = new RefreshToken(token, request.Audience, refreshTokenExpires, user!.Id);
        await _userRepository.AddRefreshToken(refreshToken);
        
        return new TokenResponse()
        {
            IsSuccess = true,
            Jwt = jwt.Token,
            JwtExpires = jwt.Expires,
            RefreshToken = token,
            RefreshTokenExpires = refreshTokenExpires
        };
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[512];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
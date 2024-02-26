using MadWorldNL.Server.Application.Mappers;
using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Application.Users;

public class ResetPasswordUseCase
{
    private readonly IUserManager _userManager;

    public ResetPasswordUseCase(IUserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<DefaultResponse> ResetPassword(string? email, string? token, string? password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(password))
        {
            return DefaultResponse.Error($"{nameof(email)}, {nameof(token)} and {nameof(password)} is required");
        }
        
        var result = await _userManager.ResetPasswordAsync(email, token, password);
        return result.ToDefaultResponse();
    }
}
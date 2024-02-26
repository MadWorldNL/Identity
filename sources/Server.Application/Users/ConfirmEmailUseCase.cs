using MadWorldNL.Server.Application.Mappers;
using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Application.Users;

public class ConfirmEmailUseCase
{
    private readonly IUserManager _userManager;

    public ConfirmEmailUseCase(IUserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<DefaultResponse> ConfirmEmail(string? email, string? token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        {
            return DefaultResponse.Error($"{nameof(email)} and {nameof(token)} is required");
        }
        
        var result = await _userManager.ConfirmEmailAsync(email, token);
        return result.ToDefaultResponse();
    }
}
using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;
using Microsoft.Extensions.Logging;

namespace MadWorldNL.Server.Application.Users;

public class ForgotPasswordUseCase
{
    private readonly ILogger<ForgotPasswordUseCase> _logger;
    private readonly IUserManager _userManager;

    public ForgotPasswordUseCase(ILogger<ForgotPasswordUseCase> logger, IUserManager userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    
    public async Task<DefaultResponse> ForgotPassword(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return DefaultResponse.Error($"{nameof(email)} is required");
        }

        var result = await _userManager.ForgotPasswordAsync(email);

        if (result.IsSuccess)
        {
            _logger.LogInformation("Forgot password {Email}: {Token}", email, result.Token);
        }

        return DefaultResponse.Success("If email exists, a reset password link will be sent to the email address.");
    }
}
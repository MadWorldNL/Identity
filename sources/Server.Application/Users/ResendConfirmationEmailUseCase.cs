using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;
using Microsoft.Extensions.Logging;

namespace MadWorldNL.Server.Application.Users;

public class ResendConfirmationEmailUseCase
{
    private readonly ILogger<ResendConfirmationEmailUseCase> _logger;
    private readonly IUserManager _userManager;

    public ResendConfirmationEmailUseCase(ILogger<ResendConfirmationEmailUseCase> logger, IUserManager userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    
    public async Task<DefaultResponse> ResendConfirmationEmail(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return DefaultResponse.Error($"{nameof(email)} is required");
        }

        var result = await _userManager.GenerateConfirmEmailToken(email);
        
        if (result.EmailExists)
        {
            _logger.LogInformation("Resend confirm email {Email}: {Token}", email, result.Token);
        }

        return DefaultResponse.Success("If email exists, a confirmation email will be sent to the email address.");
    }
}
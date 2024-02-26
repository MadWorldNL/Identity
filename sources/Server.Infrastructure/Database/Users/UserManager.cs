using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Domain.Users.ForgotPasswords;
using MadWorldNL.Server.Domain.Users.RegisterUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public sealed class UserManager : IUserManager
{
    private readonly ILogger<UserManager> _logger;
    private readonly UserManager<IdentityUserExtended> _identityUserManager;

    public UserManager(ILogger<UserManager> logger, UserManager<IdentityUserExtended> identityUserManager)
    {
        _logger = logger;
        _identityUserManager = identityUserManager;
    }
    
    public async Task<DefaultResult> CreateAsync(NewUser user)
    {
        var existingUser = await _identityUserManager.FindByEmailAsync(user.Email);
        if (existingUser is not null)
        {
            return new DefaultResult()
            {
                Message = $"User {user.Email} already exists"
            };
        }
        
        var identityUser = new IdentityUserExtended
        {
            Email = user.Email,
            UserName = user.Email,
        };
        
        var result = await _identityUserManager.CreateAsync(identityUser, user.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("A new user is {Email} is created", user.Email);
        }
        
        return new DefaultResult()
        {
            IsSuccess = result.Succeeded,
            Message = result.Errors.Any() 
                ? string.Join(", ", result.Errors.Select(e => e.Description)) 
                : string.Empty
        };
    }
    
    public async Task<DefaultResult> ConfirmEmailAsync(string email, string token)
    {
        var user = await _identityUserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return new DefaultResult()
            {
                Message = $"User {email} not found"
            };
        }
        
        var result = await _identityUserManager.ConfirmEmailAsync(user, token);
        return new DefaultResult()
        {
            IsSuccess = result.Succeeded,
            Message = result.Errors.Any() 
                ? string.Join(", ", result.Errors.Select(e => e.Description)) 
                : string.Empty
        };
    }
    
    public async Task<IIdentityUser> FindByEmailAsync(string email)
    {
        return await _identityUserManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
    }
    
    public async Task<ForgotPasswordResult> ForgotPasswordAsync(string email)
    {
        var user = await _identityUserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return new ForgotPasswordResult()
            {
                Message = $"User {email} not found"
            };
        }
        
        var token = await _identityUserManager.GeneratePasswordResetTokenAsync(user);
        return new ForgotPasswordResult()
        {
            IsSuccess = true,
            Token = token
        };
    }

    public async Task<IList<string>> GetRolesByEmailAsync(string email)
    {
        var user = await _identityUserManager.FindByEmailAsync(email);
        return await _identityUserManager.GetRolesAsync(user!);
    }

    public async Task<DefaultResult> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _identityUserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return new DefaultResult()
            {
                Message = $"User {email} not found"
            };
        }
        
        var result = await _identityUserManager.ResetPasswordAsync(user, token, newPassword);
        return new DefaultResult()
        {
            IsSuccess = result.Succeeded,
            Message = result.Errors.Any() 
                ? string.Join(", ", result.Errors.Select(e => e.Description)) 
                : string.Empty
        };
    }
}
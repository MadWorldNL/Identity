using MadWorldNL.Server.Domain;
using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Domain.Users.RegisterUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public sealed class UserManager : IUserManager
{
    private readonly ILogger<UserManager> _logger;
    private readonly UserManager<IdentityUserExtended> _identityUserManager;

    public UserManager(ILogger<UserManager> logger,UserManager<IdentityUserExtended> identityUserManager)
    {
        _logger = logger;
        _identityUserManager = identityUserManager;
    }
    
    public async Task<DefaultResponse> CreateAsync(NewUser user)
    {
        var existingUser = await _identityUserManager.FindByEmailAsync(user.Email);
        if (existingUser is not null)
        {
            return new DefaultResponse()
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
        
        return new DefaultResponse()
        {
            IsSuccess = result.Succeeded,
            Message = result.Errors.Any() 
                ? string.Join(", ", result.Errors.Select(e => e.Description)) 
                : string.Empty
        };
    }
}
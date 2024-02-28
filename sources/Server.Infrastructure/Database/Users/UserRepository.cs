using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database.Users.Mappers;
using Microsoft.EntityFrameworkCore;

namespace MadWorldNL.Server.Infrastructure.Database.Users;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }
    
    public Task AddRefreshToken(RefreshToken token)
    {
        var refreshToken = token.ToTable();
        
        _context
            .RefreshTokens
            .Add(refreshToken);
        
        return _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshToken(string token)
    {
        var refreshToken = await _context
            .RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);

        return refreshToken?.ToDetails();
    }

    public async Task<List<string>> GetRoles()
    {
        return await _context.Roles
            .AsNoTracking()
            .Select(x => x.Name!)
            .ToListAsync();
    }
}
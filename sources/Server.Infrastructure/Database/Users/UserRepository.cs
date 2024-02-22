using MadWorldNL.Server.Domain.Users;
using MadWorldNL.Server.Infrastructure.Database.Users.Mappers;

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
}
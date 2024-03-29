using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Application.Users;

public class GetUsersUserCase
{
    private readonly IUserRepository _userRepository;

    public GetUsersUserCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public IReadOnlyList<IIdentityUser> GetUsers(int page)
    {
        if (page < 0)
        {
            page = 0;
        }

        return _userRepository.GetUsers(page);
    }
}
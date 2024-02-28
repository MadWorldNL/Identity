using MadWorldNL.Server.Domain.Users;

namespace MadWorldNL.Server.Application.UserSettings;

public class GetAllRolesUseCase
{
    private readonly IUserRepository _repository;

    public GetAllRolesUseCase(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<string>> GetAllRoles()
    {
        return await _repository.GetRoles();
    }
}
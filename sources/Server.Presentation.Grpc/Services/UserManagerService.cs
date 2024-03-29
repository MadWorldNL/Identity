using Grpc.Core;
using MadWorldNL.Server.Application.Users;
using MadWorldNL.Server.Application.UserSettings;
using MadWorldNL.Server.Domain.Authorizations;
using MadWorldNL.Server.Presentation.Grpc.Mappers.Authentication;
using Microsoft.AspNetCore.Authorization;
using Server.Presentation.Grpc.UserManager.V1;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

[Authorize(Policies.RequireIdentityAdministratorRole)]
public class UserManagerService : UserManager.UserManagerBase
{
    private readonly ILogger<UserManagerService> _logger;
    private readonly GetAllRolesUseCase _getAllRolesUseCase;
    private readonly GetUsersUserCase _getUsersUserCase;

    public UserManagerService(
        ILogger<UserManagerService> logger, 
        GetAllRolesUseCase getAllRolesUseCase,
        GetUsersUserCase getUsersUserCase)
    {
        _logger = logger;
        _getAllRolesUseCase = getAllRolesUseCase;
        _getUsersUserCase = getUsersUserCase;
    }

    public override Task<DeleteSessionsResponse> DeleteSessions(DeleteSessionsRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task<GetRolesResponse> GetRoles(GetRolesRequest request, ServerCallContext context)
    {
        var roles = await _getAllRolesUseCase.GetAllRoles();
        return new GetRolesResponse()
        {
            Roles = { roles }
        };
    }

    public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<GetUsersResponse> GetUsers(GetUsersRequest request, ServerCallContext context)
    {
        var users = _getUsersUserCase.GetUsers(request.Page);
        return Task.FromResult(users.ToGetUsersResponse());
    }

    public override Task<PatchUserResponse> PatchUser(PatchUserRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
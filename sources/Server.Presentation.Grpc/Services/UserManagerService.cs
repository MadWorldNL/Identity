using Grpc.Core;
using MadWorldNL.Server.Application.UserSettings;
using MadWorldNL.Server.Domain.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

[Authorize(Policies.RequireIdentityAdministratorRole)]
public class UserManagerService : UserManager.UserManagerBase
{
    private readonly ILogger<UserManagerService> _logger;
    private readonly GetAllRolesUseCase _getAllRolesUseCase;

    public UserManagerService(ILogger<UserManagerService> logger, GetAllRolesUseCase getAllRolesUseCase)
    {
        _logger = logger;
        _getAllRolesUseCase = getAllRolesUseCase;
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
        throw new NotImplementedException();
    }

    public override Task<PatchUserResponse> PatchUser(PatchUserRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}
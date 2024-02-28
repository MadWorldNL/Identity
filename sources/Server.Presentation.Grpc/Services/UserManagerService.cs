using Grpc.Core;
using MadWorldNL.Server.Domain.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Server.Presentation.Grpc;

namespace MadWorldNL.Server.Presentation.Grpc.Services;

[Authorize(Policies.RequireAdministratorRole)]
public class UserManagerService : UserManager.UserManagerBase
{
    private readonly ILogger<UserManagerService> _logger;

    public UserManagerService(ILogger<UserManagerService> logger)
    {
        _logger = logger;
    }

    public override Task<DeleteSessionsResponse> DeleteSessions(DeleteSessionsRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<GetRolesResponse> GetRoles(GetRolesRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
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
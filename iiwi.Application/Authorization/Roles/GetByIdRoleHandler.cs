using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Account;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

public class GetByIdRoleHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<UpdateProfileHandler> _logger
    ) : IHandler<GetByIdRoleRequest, GetByIdRoleResponse>
{

    public async Task<Result<GetByIdRoleResponse>> HandleAsync(GetByIdRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        //return role is null
        //    ? Results.NotFound()
        //    : Results.Ok(new RoleDto(role.Id, role.Name, role.Description()));

        return role is null
            ? new Result<GetByIdRoleResponse>(HttpStatusCode.NotFound, new GetByIdRoleResponse
            {
                Message = $"Role with ID '{request.Id}' not found."
            })
            : new Result<GetByIdRoleResponse>(HttpStatusCode.OK, new GetByIdRoleResponse
            {
                //Id = role.Id,
                Name = role.Name,
                //Description = role.Description,
                Message = "Role found"
            });
    }
}

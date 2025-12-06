using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for getting a role by ID.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="permission">The permission repository.</param>
/// <param name="_logger">The logger.</param>
public class GetByIdRoleHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<GetByIdRoleHandler> _logger
    ) : IHandler<GetByIdRoleRequest, GetByIdRoleResponse>
{

    /// <summary>
    /// Handles the get role by ID request asynchronously.
    /// </summary>
    /// <param name="request">The get role by ID request.</param>
    /// <returns>A result containing the role response.</returns>
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

using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for updating permissions.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="permission">The permission repository.</param>
/// <param name="_logger">The logger.</param>
public class UpdatePermissionHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<PermissionHandler> _logger
    ) : IHandler<UpdatePermissionRequest, Response>
{

    /// <summary>
    /// Handles the update permission request asynchronously.
    /// </summary>
    /// <param name="request">The update permission request.</param>
    /// <returns>A result containing the response.</returns>
    public async Task<Result<Response>> HandleAsync(UpdatePermissionRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);

        if (role is null)
        {
            _logger.LogWarning("Role with ID {RoleId} not found", request.Id);
            return new Result<Response>(HttpStatusCode.NotFound, new Response
            {
                Message = "Not Found"
            });
        }
        //await permission.UpdateRolePermissionsForRoleAsync(role.Id, request.Permissions);

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Permission update successfully.",
        });
    }
}

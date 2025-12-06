using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for getting permissions.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="permission">The permission repository.</param>
/// <param name="_logger">The logger.</param>
public class PermissionHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<PermissionHandler> _logger
    ) : IHandler<PermissionRequest, PermissionResponse>
{

    /// <summary>
    /// Handles the permission request asynchronously.
    /// </summary>
    /// <param name="request">The permission request.</param>
    /// <returns>A result containing the permission response.</returns>
    public async Task<Result<PermissionResponse>> HandleAsync(PermissionRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);

        if (role is null)
        {
            _logger.LogWarning("Role with ID {RoleId} not found", request.Id);
            return new Result<PermissionResponse>(HttpStatusCode.NotFound, new PermissionResponse
            {
                Message = "Not Found"
            });
        }
        //var response = await permission.GetPermissionsByRoleIdAsync(role.Id);

        return new Result<PermissionResponse>(HttpStatusCode.OK, new PermissionResponse
        {
            Message = "Permissions list",
            Permissions = []
        });
    }
}

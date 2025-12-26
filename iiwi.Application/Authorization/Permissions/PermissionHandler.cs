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
    /// <summary>
    /// Handles a permission request by locating the role and returning its permissions summary.
    /// </summary>
    /// <param name="request">The permission request containing the role identifier to look up.</param>
    /// <returns>
    /// A Result containing a PermissionResponse:
    /// - If the role is not found: HTTP 404 with Message = "Not Found".
    /// - If the role is found: HTTP 200 with Message = "Permissions list" and an empty Permissions collection (current behavior).
    /// <summary>
    /// Handles a permission lookup request and returns a permission summary for the specified role.
    /// </summary>
    /// <param name="request">The permission request containing the role identifier to look up.</param>
    /// <returns>A Result containing a PermissionResponse: if the role is not found, HTTP 404 with Message = "Not Found"; if the role is found, HTTP 200 with Message = "Permissions list" and a Permissions collection (currently empty).</returns>
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
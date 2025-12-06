using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for getting roles.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="permission">The permission repository.</param>
/// <param name="_logger">The logger.</param>
public class RoleHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<RoleHandler> _logger
    ) : IHandler<RoleRequest, RoleResponse>
{

    /// <summary>
    /// Handles the role request asynchronously.
    /// </summary>
    /// <param name="request">The role request.</param>
    /// <returns>A result containing the role response.</returns>
    public async Task<Result<RoleResponse>> HandleAsync(RoleRequest request)
    {
        var roles = await _roleManager.Roles
                .Select(r => new Role
                {
                    //Id = r.Id,
                    Name = r.Name,
                })
                .ToListAsync();

        return new Result<RoleResponse>(HttpStatusCode.OK, new RoleResponse
        {
            Roles = roles,
            Message = "List of roles"
        });
    }
}

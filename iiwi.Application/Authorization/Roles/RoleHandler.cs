using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Account;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
using System.Net;

namespace iiwi.Application.Authorization;

public class RoleHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<UpdateProfileHandler> _logger
    ) : IHandler<RoleRequest, RoleResponse>
{

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

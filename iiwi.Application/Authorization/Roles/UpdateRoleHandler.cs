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

public class UpdateRoleRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<UpdateProfileHandler> _logger
    ) : IHandler<UpdateRoleRequest, Response>
{

    public async Task<Result<Response>> HandleAsync(UpdateRoleRequest request)
    {
        var roles = await _roleManager.Roles
                .Select(r => new Role
                {
                    //Id = r.Id,
                    Name = r.Name,
                })
                .ToListAsync();

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Role Update Successfully."
        });
    }
}

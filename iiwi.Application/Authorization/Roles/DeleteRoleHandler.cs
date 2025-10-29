using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

public class DeleteRoleRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<DeleteRoleRequestHandler> _logger
    ) : IHandler<DeleteRoleRequest, Response>
{

    public async Task<Result<Response>> HandleAsync(DeleteRoleRequest request)
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

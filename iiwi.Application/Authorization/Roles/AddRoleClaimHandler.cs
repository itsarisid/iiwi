using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
using System.Net;
using static iiwi.Library.Helper;

namespace iiwi.Application.Authorization;

public class AddRoleClaimRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<AddRoleClaimRequestHandler> _logger
    ) : IHandler<AddRoleClaimRequest, Response>
{

    public async Task<Result<Response>> HandleAsync(AddRoleClaimRequest request)
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

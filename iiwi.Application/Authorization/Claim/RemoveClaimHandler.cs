using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

public class RemoveClaimHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<RemoveClaimHandler> _logger
    ) : IHandler<RemoveClaimRequest, Response>
{

    public async Task<Result<Response>> HandleAsync(RemoveClaimRequest request)
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
            Message = "Role Removed Successfully."
        });
    }
}

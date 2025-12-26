using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for deleting a role.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="_logger">The logger.</param>
public class DeleteRoleRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<DeleteRoleRequestHandler> _logger
    ) : IHandler<DeleteRoleRequest, Response>
{

    /// <summary>
    /// Handles the delete role request asynchronously.
    /// </summary>
    /// <param name="request">The delete role request.</param>
    /// <summary>
    /// Handles a delete role request and produces a success response.
    /// </summary>
    /// <param name="request">The delete role request to process.</param>
    /// <summary>
    /// Handles a delete-role request and returns a success response.
    /// </summary>
    /// <returns>A Result containing HTTP 200 and a Response whose Message is "Role Update Successfully."</returns>
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
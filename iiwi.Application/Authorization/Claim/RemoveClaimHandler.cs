using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for removing a claim.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="_logger">The logger.</param>
public class RemoveClaimHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<RemoveClaimHandler> _logger
    ) : IHandler<RemoveClaimRequest, Response>
{

    /// <summary>
    /// Handles the remove claim request asynchronously.
    /// </summary>
    /// <param name="request">The remove claim request.</param>
    /// <summary>
    /// Handles a request to remove a claim by querying existing roles and returning an outcome message.
    /// </summary>
    /// <param name="request">The request containing details of the claim to remove.</param>
    /// <returns>A Result containing a Response with HTTP 200 OK and a message indicating the role removal outcome.</returns>
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
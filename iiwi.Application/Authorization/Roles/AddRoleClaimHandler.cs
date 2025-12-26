using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for adding a role claim.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="_logger">The logger.</param>
public class AddRoleClaimRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<AddRoleClaimRequestHandler> _logger
    ) : IHandler<AddRoleClaimRequest, Response>
{

    /// <summary>
    /// Handles the add role claim request asynchronously.
    /// </summary>
    /// <param name="request">The add role claim request.</param>
    /// <summary>
    /// Handles an AddRoleClaimRequest and returns a response indicating the outcome of the operation.
    /// </summary>
    /// <param name="request">The request containing details for adding a role claim.</param>
    /// <summary>
    /// Handles an add-role-claim request by retrieving roles and returning a success response.
    /// </summary>
    /// <param name="request">The AddRoleClaimRequest that triggered the operation; the handler does not inspect the request's payload.</param>
    /// <returns>A Result containing a Response with HTTP status code 200 (OK) and a message indicating the role update succeeded.</returns>
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
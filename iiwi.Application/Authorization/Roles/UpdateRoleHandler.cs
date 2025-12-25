using DotNetCore.Mediator;
using DotNetCore.Results;

using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

/// <summary>
/// Handler for updating a role.
/// </summary>
/// <param name="_roleManager">The role manager.</param>
/// <param name="_logger">The logger.</param>
public class UpdateRoleRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<UpdateRoleRequestHandler> _logger
    ) : IHandler<UpdateRoleRequest, Response>
{

    /// <summary>
    /// Handles the update role request asynchronously.
    /// </summary>
    /// <param name="request">The update role request.</param>
    /// <summary>
    /// Handles updating an existing role identified by the request's Id.
    /// </summary>
    /// <param name="request">Contains the role Id (expected to be populated from URL parameters) and the new Name to apply to the role.</param>
    /// <returns>A Result containing a Response with an HTTP status and message: 404 if the role is not found, 400 with aggregated identity errors if the update fails, or 200 on success.</returns>
    public async Task<Result<Response>> HandleAsync(UpdateRoleRequest request)
    {
        // Expecting Id to be set via URL params merge (Helper.MergeParameters supports non-public props)
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role is null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response { Message = "Role not found." });
        }

        // Update fields
        role.Name = request.Name;
        role.NormalizedName = request.Name?.ToUpperInvariant();
        // If you store Description on ApplicationRole, set it here as well

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var error = string.Join("; ", result.Errors.Select(e => $"{e.Code}:{e.Description}"));
            return new Result<Response>(HttpStatusCode.BadRequest, new Response { Message = error });
        }

        _logger.LogInformation("Role {RoleId} updated.", role.Id);
        return new Result<Response>(HttpStatusCode.OK, new Response { Message = "Role updated successfully." });
    }
}
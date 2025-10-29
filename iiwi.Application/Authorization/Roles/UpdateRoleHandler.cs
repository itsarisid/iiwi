using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Account;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace iiwi.Application.Authorization;

public class UpdateRoleRequestHandler(
    RoleManager<ApplicationRole> _roleManager,
    ILogger<UpdateProfileHandler> _logger
    ) : IHandler<UpdateRoleRequest, Response>
{

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

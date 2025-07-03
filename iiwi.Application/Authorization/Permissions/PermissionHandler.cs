using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authorization;

public class PermissionHandler(
    RoleManager<ApplicationRole> _roleManager,
    IPermissionRepository permission,
    ILogger<PermissionHandler> _logger
    ) : IHandler<PermissionRequest, PermissionResponse>
{

    public async Task<Result<PermissionResponse>> HandleAsync(PermissionRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);

        if (role is null)
        {
            _logger.LogWarning("Role with ID {RoleId} not found", request.Id);
            return new Result<PermissionResponse>(HttpStatusCode.NotFound, new PermissionResponse
            {
                Message = "Not Found"
            });
        }
        var claims = await _roleManager.GetClaimsAsync(role);
        var response = claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value);

        return new Result<PermissionResponse>(HttpStatusCode.NotFound, new PermissionResponse
        {
            Message = "Not Found",
            Permissions = response
        });
    }
}

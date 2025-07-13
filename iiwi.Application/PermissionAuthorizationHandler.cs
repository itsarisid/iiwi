using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace iiwi.Application;

// Custom authorization handler
public class PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger) : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
        {
            logger.LogInformation("Authorization requirement satisfied");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogWarning("Authorization requirement not satisfied for permission: {Permission}", requirement.Permission);
            context.Fail();
        }

        return Task.CompletedTask;
    }
}

public record PermissionRequirement(string Permission) : IAuthorizationRequirement;

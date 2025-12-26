using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace iiwi.Application;

/// <summary>
/// Custom authorization handler that checks if the user has the required permission claim.
/// </summary>
/// <param name="logger">The logger instance.</param>
public class PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger) : AuthorizationHandler<PermissionRequirement>
{
    /// <summary>
    /// Handles the authorization requirement asynchronously.
    /// </summary>
    /// <param name="context">The authorization handler context.</param>
    /// <summary>
    /// Evaluates whether the current user satisfies the specified permission requirement and updates the authorization context accordingly.
    /// </summary>
    /// <param name="context">The authorization context containing the user and the mechanism to mark the requirement as succeeded or failed.</param>
    /// <param name="requirement">The permission requirement to evaluate.</param>
    /// <summary>
    /// Evaluates whether the current user has the required permission and updates the authorization context to succeed or fail accordingly.
    /// </summary>
    /// <param name="context">The authorization context containing the user principal and evaluation state.</param>
    /// <param name="requirement">The permission requirement to validate against the user's claims.</param>
    /// <returns>A completed task after the requirement has been evaluated and the authorization context has been updated.</returns>
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

/// <summary>
/// Represents a requirement for a specific permission.
/// </summary>
/// <param name="Permission">The required permission string.</param>
public record PermissionRequirement(string Permission) : IAuthorizationRequirement;
using iiwi.Common;
using iiwi.Database;
using Microsoft.EntityFrameworkCore;

namespace iiwi.NetLine.Extensions;

/// <summary>
/// Provides extension methods for configuring authorization policies
/// </summary>
/// <remarks>
/// These extensions help automate the registration of authorization policies
/// from both static definitions and dynamic database sources.
/// </remarks>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Automatically registers authorization policies from all available sources
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection for method chaining</returns>
    /// <remarks>
    /// <para>
    /// This method performs the following operations:
    /// 1. Registers policies from the static <see cref="Permissions"/> class
    ///    - Each permission becomes a policy requiring that specific claim
    /// 2. Optionally registers policies from the database
    ///    - Queries the Permission table for dynamic permissions
    ///    - Only adds policies that don't already exist
    /// </para>
    /// <para>
    /// Usage Example:
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.Services.AddAutoPolicies();
    /// </code>
    /// </para>
    /// <para>
    /// Note: Database permissions are only checked once at startup.
    /// For dynamic permission changes, consider implementing a cache refresh mechanism.
    /// </para>
    /// </remarks>
    public static IServiceCollection AddAutoPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Register all permissions from static class
            foreach (var permission in Permissions.GetAll())
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim(Permissions.Permission, permission));
            }

            // Register permissions from database (if any)
            using IServiceScope scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var dbPermissions = dbContext.Permission;

            foreach (var permission in dbPermissions)
            {
                var codeName = permission.CodeName;

                // Only add if not already registered
                if (options.GetPolicy(codeName) is null)
                {
                    options.AddPolicy(codeName, policy =>
                        policy.RequireClaim(Permissions.Permission, codeName));
                }
            }
        });

        return services;
    }
}
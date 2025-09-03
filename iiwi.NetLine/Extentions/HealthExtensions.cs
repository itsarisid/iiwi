using iiwi.Common;
using iiwi.Database;
using iiwi.NetLine.Health;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace iiwi.NetLine.Extensions;

/// <summary>
/// Provides extension methods for configuring application health checks
/// </summary>
/// <remarks>
/// These extensions configure comprehensive health monitoring for:
/// - Database connectivity
/// - Application startup status
/// - Entity Framework Core DbContext
/// - Custom health checks
/// 
/// Health checks are exposed via standard endpoints that can be:
/// - Monitored by container orchestrators
/// - Integrated with load balancers
/// - Used for readiness/liveness probes
/// </remarks>
public static class HealthExtensions
{
    /// <summary>
    /// Adds standard health check services to the application builder
    /// </summary>
    /// <typeparam name="TBuilder">The application builder type</typeparam>
    /// <param name="builder">The host application builder</param>
    /// <returns>The original builder for method chaining</returns>
    /// <remarks>
    /// <para>
    /// Configures the following health checks:
    /// 1. Database connectivity check (SQL Server)
    /// 2. Startup completion check
    /// 3. Entity Framework Core DbContext check
    /// 4. Custom health checks
    /// </para>
    /// <para>
    /// Health checks are tagged for different purposes:
    /// - "sample": Basic health indicators
    /// - "ready": Startup completion status
    /// </para>
    /// <para>
    /// The database connection string is read from configuration using
    /// the default connection string name from <see cref="General.DbConnectionName"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.AddAppHealthChecks();
    /// </code>
    /// </example>
    public static TBuilder AddAppHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        // Get database connection string from configuration
        var dbConnection = builder.Configuration.GetConnectionString(General.DbConnectionName);
        ArgumentNullException.ThrowIfNullOrEmpty(dbConnection);

        // Configure health check services
        builder.Services.AddHealthChecks()
            // Custom database health check
            .AddCheck<DbHealthCheck>("DbHealth",
                failureStatus: HealthStatus.Degraded,
                tags: ["sample"])

            // Startup completion check
            .AddCheck<StartupHealthCheck>("Startup",
                failureStatus: HealthStatus.Degraded,
                tags: ["ready"])

            // SQL Server connectivity check
            .AddSqlServer(dbConnection)

            // EF Core DbContext health check
            .AddDbContextCheck<ApplicationDbContext>();

        return builder;
    }
}
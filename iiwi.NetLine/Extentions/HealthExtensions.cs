using iiwi.Common;
using iiwi.Database;
using iiwi.NetLine.Health;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace iiwi.NetLine.Extentions;

/// <summary>
/// Adds common .NET Aspire services: service health checks.
/// To learn more about using this project, see https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0
/// </summary>
public static class HealthExtensions
{
    public static TBuilder AddAppHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        var dbConnection = builder.Configuration.GetConnectionString(General.DbConnectionName);

        ArgumentNullException.ThrowIfNullOrEmpty(dbConnection);

        builder.Services.AddHealthChecks()
            .AddCheck<DbHealthCheck>("DbHealth",
                failureStatus: HealthStatus.Degraded,
                tags: ["sample"])
            .AddCheck<StartupHealthCheck>("Startup",
                failureStatus: HealthStatus.Degraded,
                tags: ["ready"])
            .AddSqlServer(dbConnection)
            .AddDbContextCheck<ApplicationDbContext>(); ;


        return builder;
    }
}

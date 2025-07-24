using iiwi.NetLine.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SwaggerThemes;

namespace iiwi.NetLine.Config;

/// <summary>
/// Configures environment-specific middleware pipeline and health check endpoints
/// </summary>
/// <remarks>
/// This class sets up different behavior for development/production versus other environments,
/// including Swagger UI, health checks, and error handling.
/// </remarks>
public static class EnvironmentSetup
{
    /// <summary>
    /// Configures the environment-specific middleware pipeline
    /// </summary>
    /// <param name="app">The WebApplication instance to configure</param>
    /// <returns>The configured application builder</returns>
    /// <remarks>
    /// <para>
    /// Development/Production Environment Configuration:
    /// 1. Enables Swagger UI with custom Gruvbox theme
    /// 2. Configures comprehensive health check endpoints:
    ///    - /healthz: Overall system health (all checks)
    ///    - /healthz/ready: Readiness checks (tagged with "ready")
    ///    - /healthz/live: Liveness endpoint (no checks)
    ///    - /healthz/alive: Basic liveness checks (tagged with "live")
    /// 3. Uses custom JSON response formatting for health checks
    /// </para>
    /// <para>
    /// Other Environments Configuration:
    /// 1. Enables HSTS with default 30-day policy
    /// 2. Configures global exception handler
    /// </para>
    /// <para>
    /// Health Check Status Mappings:
    /// - Healthy: 200 OK
    /// - Degraded: 200 OK (with details)
    /// - Unhealthy: 503 Service Unavailable
    /// </para>
    /// </remarks>
    public static IApplicationBuilder MapEnvironment(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            // Enable Swagger documentation in both dev and production
            app.UseSwagger();
            app.UseSwaggerUI(Theme.Gruvbox, @"
                .swagger-ui .btn.execute {
                    background-color: #89bf04;
                    border-color: #89bf04;
                    color: #fff;
                }");

            // Comprehensive health check endpoint
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                AllowCachingResponses = true,
                ResponseWriter = HealthCheckerResponse.WriteResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
            });

            // Readiness check - for startup completion
            app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });

            // Basic liveness check - minimal endpoint
            app.MapHealthChecks("/healthz/live", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            // Extended liveness check - tagged services
            app.MapHealthChecks("healthz/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live"),
                ResponseWriter = HealthCheckerResponse.WriteResponse,
            });
        }
        else
        {
            // Production-grade security for non-dev environments
            app.UseHsts();
            app.UseExceptionHandler(exceptionHandlerApp =>
                exceptionHandlerApp.Run(async context =>
                    await Results.Problem().ExecuteAsync(context)));
        }

        return app;
    }
}
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace iiwi.NetLine.Health;

/// <summary>
/// Health check that monitors application startup completion status
/// </summary>
/// <remarks>
/// This health check reports whether critical application initialization
/// tasks have completed. It transitions from Unhealthy to Healthy state
/// once all startup operations are finished.
/// 
/// Typical usage:
/// 1. Application starts with StartupCompleted = false
/// 2. Background services complete initialization
/// 3. StartupCompleted is set to true
/// 4. Health check begins reporting Healthy status
/// </remarks>
public class StartupHealthCheck : IHealthCheck
{
    private volatile bool _isReady;

    /// <summary>
    /// Gets or sets whether application startup has completed
    /// </summary>
    /// <value>
    /// True if all critical startup tasks have finished, false otherwise
    /// </value>
    /// <remarks>
    /// This property should be set by your application's startup background
    /// service once all initialization tasks are complete. The setter is
    /// thread-safe due to volatile backing field.
    /// </remarks>
    public bool StartupCompleted
    {
        get => _isReady;
        set => _isReady = value;
    }

    /// <summary>
    /// Checks the application's startup status
    /// </summary>
    /// <param name="context">The health check context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// HealthCheckResult.Healthy if startup completed,
    /// HealthCheckResult.Unhealthy if still initializing
    /// </returns>
    /// <remarks>
    /// This method is called periodically by the health check service.
    /// It provides clear status messages indicating whether:
    /// - The application is ready to handle requests (Healthy)
    /// - Startup tasks are still running (Unhealthy)
    /// </remarks>
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        if (StartupCompleted)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("The startup task has completed."));
        }

        return Task.FromResult(
            HealthCheckResult.Unhealthy("The startup task is still running."));
    }
}
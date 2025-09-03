using iiwi.NetLine.Health;

namespace iiwi.NetLine.Services;

/// <summary>
/// Background service that simulates application startup initialization tasks
/// and reports completion status through health checks.
/// </summary>
/// <remarks>
/// This service is used to delay health check "healthy" status until critical
/// startup initialization is complete.
/// </remarks>
public class StartupBackgroundService : BackgroundService
{
    private readonly StartupHealthCheck _healthCheck;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartupBackgroundService"/> class.
    /// </summary>
    /// <param name="healthCheck">The health check service to report startup status to.</param>
    public StartupBackgroundService(StartupHealthCheck healthCheck)
        => _healthCheck = healthCheck;

    /// <summary>
    /// Executes the background service's main logic.
    /// </summary>
    /// <param name="stoppingToken">Triggered when the service is being stopped.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Simulate a long-running startup initialization process
        // (e.g., database warmup, cache loading, connection pooling)
        await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

        // Signal to health checks that startup initialization is complete
        _healthCheck.StartupCompleted = true;
    }
}
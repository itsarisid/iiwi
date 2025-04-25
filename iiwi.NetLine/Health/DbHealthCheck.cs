using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace iiwi.NetLine.Health;

/// <summary>
/// The health check's logic is placed in the CheckHealthAsync method. 
/// The preceding example sets a dummy variable, isHealthy, to true. 
/// If the value of isHealthy is set to false, the HealthCheckRegistration.FailureStatus status is returned.
/// </summary>
public class DbHealthCheck: IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        // ...

        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}

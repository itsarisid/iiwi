using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace iiwi.NetLine.Health;

public class HealthCheckWithDI : IHealthCheck
{
    //private readonly SampleHealthCheckWithDiConfig _config;

    //public HealthCheckWithDI(SampleHealthCheckWithDiConfig config)
    //    => _config = config;

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        // use _config ...

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

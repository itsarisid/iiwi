using iiwi.NetLine.Extentions;
using iiwi.NetLine.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SwaggerThemes;

namespace iiwi.NetLine.Config;

public static class EnvironmentSetup
{
    public static IApplicationBuilder MapEnvironment(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(Theme.UniversalDark);

            // All health checks must pass for app to be considered ready to accept traffic after starting
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

            // For the readiness check. The readiness check filters health checks to those tagged with ready
            app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });

            // For the liveness check. The liveness check filters out all health checks by returning false in the HealthCheckOptions.Predicate delegate. 
            app.MapHealthChecks("/healthz/live", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("healthz/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live"),
                ResponseWriter = HealthCheckerResponse.WriteResponse,
            });
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseExceptionHandler(exceptionHandlerApp
                => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
        }

        return app;
    }

}

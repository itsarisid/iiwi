using iiwi.NetLine.Extentions;
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

            app.UseSwaggerUI(Theme.Gruvbox, @"
                .swagger-ui .btn.execute {
                    background-color: #89bf04;
                    border-color: #89bf04;
                    color: #fff;
                }");

            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                AllowCachingResponses = true,
                ResponseWriter = HealthExtensions.WriteResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
            });

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live"),
                ResponseWriter = HealthExtensions.WriteResponse,
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

using IGeekFan.AspNetCore.RapiDoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace iiwi.AppWire.Configurations;

/// <summary>App ENV Setup</summary>
public static class EnvironmentSetup
{
    /// <summary>Uses the environment.</summary>
    /// <param name="app">The application.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.ArgumentNullException">app</exception>
    public static IApplicationBuilder UseEnvironment(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            //TODO: Development

            // Add OpenAPI 3.0 document serving middleware
            // Available at: http://localhost:<port>/swagger/v1/swagger.json
            app.UseOpenApi();
            // Add web UIs to interact with the document
            // Available at: http://localhost:<port>/swagger
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "iiwi");
                c.InjectStylesheet("/swagger-ui/swagger-dark.css");
            });

            // Add ReDoc UI to interact with the document
            // Available at: http://localhost:<port>/redoc
            app.UseRapiDocUI(options =>
            {
                options.RoutePrefix = ""; // serve the UI at root
                options.SwaggerEndpoint("swagger/v1/swagger.json", "iiwi");
                //https://mrin9.github.io/RapiDoc/api.html
                //This Config Higher priority
                options.GenericRapiConfig = new GenericRapiConfig
                {
                    RenderStyle = "focused",// view | read | focused
                    Theme = "dark",//light | dark
                    SchemaStyle = "tree",//tree | table
                    ShowMethodInNavBar = "as-colored-text", // false | as-plain-text | as-colored-text | as-colored-block
                    UsePathInNavBar = true
                };
            });
        }
        return app;
    }
}

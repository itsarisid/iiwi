using Asp.Versioning.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace iiwi.NetLine.Swagger;

public static class SwaggerUIExtensions
{
    public static SwaggerUIOptions ConfigureApiEndpoints(this SwaggerUIOptions options, WebApplication app)
    {
        var descriptions = app.DescribeApiVersions();

        if (!descriptions.Any())
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        }

        descriptions
        .OrderBy(VersionOrder)
        .ToList()
        .ForEach(desc => options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName));

        options.EnablePersistAuthorization();
        options.EnableTryItOutByDefault();
        options.DisplayRequestDuration();
        return options;
    }

    private static int VersionOrder(ApiVersionDescription desc)
    {
        return desc.GroupName switch
        {
            "v1" => 1,
            "v2" => 2,
            "v3" => 3,
            _ => 99
        };
    }
}

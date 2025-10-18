using Asp.Versioning;
using iiwi.Model.Enums;
using iiwi.NetLine.API;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;


public class PingPongModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty)
            .WithGroup(PingPong.Group)
            .AddEndpointFilter<ExceptionHandlingFilter>();

        routeGroup.MapTestEndpoint(new TestEndpointConfiguration
        {
            Author = "Sajid Khan",
            Version = "1.0.0",
            DeprecatedVersions = [0.9],
            ActiveVersions = [new ApiVersion(1, 0)],
            CachePolicy = "DefaultPolicy",
            EnableHttpLogging = true
        });

        routeGroup.MapVersionedEndpoint<EmptyRequest, SystemInfoResponse>(
            new EndpointConfiguration<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = PingPong.SystemInfoEndpoint,
                HttpMethod = HttpVerb.Get,
                ActiveVersions = [new ApiVersion(1, 0), new ApiVersion(2, 0)],
                DeprecatedVersions = [0.9],
                RequestDelegate = HandleSystemInfo,
                EnableCaching = true,
                CachePolicy = CachePolicy.DefaultPolicy,
                EnableHttpLogging = true,
                EndpointFilters = ["ExceptionHandlingFilter"]
            });

    }
    private static IResult HandleSystemInfo(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        return TypedResults.Ok(new SystemInfoResponse
        {
            Author = "Sajid Khan",
            Environment = environment.EnvironmentName
        });
    }
}


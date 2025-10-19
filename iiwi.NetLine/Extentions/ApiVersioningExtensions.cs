using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.Model;
using iiwi.NetLine.Builders;

namespace iiwi.NetLine.Extentions;

public static class ApiVersioningExtensions
{
    public static void ConfigureApiVersioning(this ApiVersioningOptions options, ApiVersioningConfig config)
    {
        options.AssumeDefaultVersionWhenUnspecified = config.AssumeDefaultVersion;
        options.DefaultApiVersion = config.DefaultApiVersion;
        options.ReportApiVersions = config.ReportApiVersions;

        if (config.EnableMultipleVersionReaders)
        {
            options.ApiVersionReader = CreateVersionReaders();
        }

        if (config.EnableSunsetPolicy)
        {
            ConfigureSunsetPolicy(options, config);
        }
    }

    public static IApiVersionReader CreateVersionReaders()
    {
        return ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("x-version")
        );
    }

    public static void ConfigureSunsetPolicy(this ApiVersioningOptions options, ApiVersioningConfig config)
    {
        options.Policies.Sunset(config.SunsetVersion)
            .Effective(DateTimeOffset.Now.AddDays(config.SunsetDays))
            .Link(config.PolicyLink)
            .Title(config.PolicyTitle)
            .Type(config.PolicyContentType);
    }

    public static void ConfigureApiExplorer(this ApiExplorerOptions options, ApiVersioningConfig config)
    {
        options.GroupNameFormat = config.GroupNameFormat;
        options.SubstituteApiVersionInUrl = config.SubstituteInUrl;
    }

    // Configuration class for better maintainability
    public class ApiVersioningConfig
    {
        public bool AssumeDefaultVersion { get; set; } = true;
        public ApiVersion DefaultApiVersion { get; set; } = new ApiVersion(1, 0);
        public bool ReportApiVersions { get; set; } = true;
        public bool EnableMultipleVersionReaders { get; set; } = true;
        public bool EnableSunsetPolicy { get; set; } = true;
        public double SunsetVersion { get; set; } = 0.9;
        public int SunsetDays { get; set; } = 60;
        public string PolicyLink { get; set; } = "policy.html";
        public string PolicyTitle { get; set; } = "Versioning Policy";
        public string PolicyContentType { get; set; } = "text/html";
        public string GroupNameFormat { get; set; } = "'v'VVV";
        public bool SubstituteInUrl { get; set; } = true;
    }

    public static ApiVersionSet CreateApiVersionSet(IEndpointRouteBuilder endpoints)
    {
        return endpoints.NewApiVersionSet()
            .HasDeprecatedApiVersion(0.9)
            .HasApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();
    }

    public static RouteHandlerBuilder WithApiVersion(this RouteHandlerBuilder builder, ApiVersionSet apiVersion)
    {
        return builder.WithApiVersionSet(apiVersion)
            .MapToApiVersion(new ApiVersion(1, 0))
            .MapToApiVersion(new ApiVersion(2, 0));
    }

    public static ApiVersionSet CreateApiVersionSet<TEndpoint, TResponse>(
        this IEndpointRouteBuilder endpoints,
        Configure<TEndpoint, TResponse> configuration)
        where TEndpoint : class
        where TResponse : class, new()
    {
        var versionSetBuilder = endpoints.NewApiVersionSet();

        foreach (var deprecatedVersion in configuration.DeprecatedVersions)
        {
            versionSetBuilder.HasDeprecatedApiVersion(deprecatedVersion);
        }

        foreach (var apiVersion in configuration.ActiveVersions)
        {
            versionSetBuilder.HasApiVersion(apiVersion);
        }

        return versionSetBuilder.ReportApiVersions().Build();
    }
}

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.Library;
using iiwi.NetLine.Builders;

namespace iiwi.NetLine.Extensions;

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

    private static Configure<TRequest, TResponse> GetApiVersions<TRequest, TResponse>(Configure<TRequest, TResponse> configuration)
        where TRequest : class
        where TResponse : class, new()
    {

        if (configuration.ActiveVersions == null || configuration.ActiveVersions.Length == 0)
        {
            configuration.ActiveVersions = [new ApiVersion(1, 0), new ApiVersion(2, 0)];
        }
        configuration.DeprecatedVersions ??= [];
        return configuration;
    }

    public static ApiVersionSet CreateApiVersionSet<TRequest, TResponse>(
        this IEndpointRouteBuilder endpoints,
        Configure<TRequest, TResponse> configuration)
        where TRequest : class
        where TResponse : class, new()
    {
        configuration = GetApiVersions(configuration);

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

    public static Delegate HandleDelegate<TRequest, TResponse>(
        this IEndpointRouteBuilder endpoints,
        Configure<TRequest, TResponse> configuration)
        where TRequest : class, new()
        where TResponse : class, new()
    {
        if (configuration.HasUrlParameters)
        {
            return CreateHandlerWithParameterWithoutRequest(configuration);
        }

            return IsEmptyRequest<TRequest>()
                    ? CreateHandlerWithoutRequest(configuration)
                    : CreateHandlerWithRequest(configuration);
    }

    public static Delegate HandleDelegate<TUrlParams,TRequest, TResponse>(
        this IEndpointRouteBuilder endpoints,
        Configure<TRequest, TResponse> configuration)
        where TUrlParams : class, new()
        where TRequest : class, new()
        where TResponse : class, new()
    {
        {
            // Both URL parameters and body
            return CreateHandlerWithParameterAndRequest<TUrlParams, TRequest, TResponse>(configuration);
        }
        if (configuration.HasUrlParameters)
        {
            return CreateHandlerWithParameterWithoutRequest(configuration);
        }

            return IsEmptyRequest<TRequest>()
                    ? CreateHandlerWithoutRequest(configuration)
                    : CreateHandlerWithRequest(configuration);
    }

    private static bool IsEmptyRequest<TRequest>() where TRequest : class
    {
        return typeof(TRequest).GetProperties().Length == 0;
    }
    private static Delegate CreateHandlerWithoutRequest<TRequest, TResponse>(
    Configure<TRequest, TResponse> configuration)
        where TRequest : class, new()
        where TResponse : class, new()
    {
        return (IMediator mediator) => new EndpointHandler<TRequest, TResponse>(mediator).HandleDelegate();
    }

    private static Delegate CreateHandlerWithParameterWithoutRequest<TRequest, TResponse>(
    Configure<TRequest, TResponse> configuration)
        where TRequest : class, new()
        where TResponse : class, new()
    {
        return (IMediator mediator, [AsParameters] TRequest request) => new EndpointHandler<TRequest, TResponse>(mediator).HandleDelegate(request);
    }
    private static Delegate CreateHandlerWithParameterAndRequest<TUrlParams, TRequest, TResponse>(
    Configure<TRequest, TResponse> configuration)
        where TUrlParams : class, new()
        where TRequest : class, new()
        where TResponse : class, new()
    {
        return (IMediator mediator, [AsParameters] TUrlParams urlParams, TRequest body) =>
        {
            //var combinedRequest = Helper.CombineParameters(urlParams, body);
            var combinedRequest = Helper.MergeParameters(urlParams, body);
            return new EndpointHandler<TRequest, TResponse>(mediator).HandleDelegate(combinedRequest);
        };
    }

    private static Delegate CreateHandlerWithRequest<TRequest, TResponse>(
        Configure<TRequest, TResponse> configuration)
        where TRequest : class, new()
        where TResponse : class, new()
    {
        return (IMediator mediator, TRequest request) => new EndpointHandler<TRequest, TResponse>(mediator).HandleDelegate(request);
    }

}

using iiwi.NetLine.Policies;

namespace iiwi.NetLine.Config;

/// <summary>
/// Provides extension methods for configuring application output caching
/// </summary>
/// <remarks>
/// This static class configures response caching policies for:
/// - Default base caching behavior
/// - Path-specific caching rules
/// - Custom policy variations
/// 
/// Supports both time-based and conditional caching strategies.
/// </remarks>
public static class CacheSetup
{
    /// <summary>
    /// Configures output caching services with predefined policies
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection</returns>
    /// <remarks>
    /// <para>
    /// Configures the following caching policies:
    /// 1. <b>Default Base Policy</b>:
    ///    - 10 second expiration for all cacheable responses
    ///    
    /// 2. <b>Path-Specific Policy</b>:
    ///    - 15 second expiration for paths starting with "/test"
    ///    - Tagged with "tag-test" for selective invalidation
    ///    
    /// 3. <b>Global Tag Policy</b>:
    ///    - Tags all responses with "tag-all" for bulk operations
    ///    
    /// 4. <b>Named Policies</b>:
    ///    - "DefaultPolicy": 25 second expiration (override base)
    ///    - "Query": Varies by "culture" query parameter
    ///    - "NoCache": Disables caching completely
    ///    - "NoLock": Disables cache locking for high concurrency
    /// </para>
    /// <para>
    /// Example Usage:
    /// Apply policies to endpoints using:
    /// <code>
    /// [OutputCache(PolicyName = "Query")]
    /// public IActionResult Get() { ... }
    /// </code>
    /// </para>
    /// </remarks>
    public static IServiceCollection AddAppCache(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOutputCache(options =>
        {
            // Default cache policy for all endpoints
            options.AddBasePolicy(builder =>
                builder.Expire(TimeSpan.FromSeconds(10)));

            // Special policy for /test routes
            options.AddBasePolicy(builder => builder
                .With(c => c.HttpContext.Request.Path.StartsWithSegments("/test"))
                .Tag("tag-test")
                .Expire(TimeSpan.FromSeconds(15)));

            // Global tagging policy
            options.AddBasePolicy(builder => builder.Tag("tag-all"));

            // Named policy variations
            options.AddPolicy("DefaultPolicy", builder =>
                builder.Expire(TimeSpan.FromSeconds(25)));
            options.AddPolicy("Query", builder =>
                builder.SetVaryByQuery("culture"));
            options.AddPolicy("NoCache", builder =>
                builder.NoCache());
            options.AddPolicy("NoLock", builder =>
                builder.SetLocking(false));
        });

        // Example of custom policy (commented out)
        // options.AddPolicy("CachePost", IiwiPolicy.Instance);

        return services;
    }
}
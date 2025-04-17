namespace iiwi.NetLine.Config;

public static class CacheSetup
{
    public static IServiceCollection AddAppCache(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder =>
                builder.Expire(TimeSpan.FromSeconds(10)));

            options.AddBasePolicy(builder => builder
                .With(c => c.HttpContext.Request.Path.StartsWithSegments("/test"))
                .Tag("tag-test")
                .Expire(TimeSpan.FromSeconds(15)));

            options.AddBasePolicy(builder => builder.Tag("tag-all"));
            options.AddPolicy("DefaultPolicy", builder =>builder.Expire(TimeSpan.FromSeconds(25)));
            options.AddPolicy("Query", builder => builder.SetVaryByQuery("culture"));
            options.AddPolicy("NoCache", builder => builder.NoCache());
            options.AddPolicy("NoLock", builder => builder.SetLocking(false));
        });
        return services;
    }
}

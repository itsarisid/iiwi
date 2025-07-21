using Audit.Core;
using Audit.Core.Providers;
using Audit.EntityFramework.Providers;
using Audit.WebApi;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace iiwi.NetLine.Config;

public static class AuditTrailSetup
{
    /// <summary>Add the audit trail.</summary>
    /// <param name="app">The application.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.ArgumentNullException">app</exception>
    public static void AddAuditTrail(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Configuration.JsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true
        };

        Audit.EntityFramework.Configuration.Setup()
               .ForContext<ApplicationDbContext>(config => config
               .ForEntity<ApplicationUser>(_ => _
                .Ignore(user => user.ConcurrencyStamp)
                .Override(user => user.PasswordHash, null)
                .Format(user => user.Gender, pass => new String('*', pass.Length)))
               .IncludeEntityObjects()
               .AuditEventType("{context}:{database}"))
               .UseOptOut()
                   .IgnoreAny(t => t.Name.EndsWith("History"));

        Configuration.Setup()
            .AuditDisabled(false) // Enable auditing
            .IncludeActivityTrace()
            .IncludeStackTrace()
            .UseConditional(c => c
                .When(ev => ev.EventType.StartsWith("HTTP"), new FileDataProvider(cfg => cfg
                    .Directory(@"D:\logs")
                    .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}.json")))
                .Otherwise(new EntityFrameworkDataProvider(_ => _
                .AuditTypeMapper(t => typeof(Domain.Logs.AuditLog))
                .AuditEntityAction<Domain.Logs.AuditLog>((ev, entry, entity) =>
                {
                    entity.ChangedData = entry.ToJson();
                    entity.EntityType = entry.EntityType.Name;
                    entity.EntityName = entry.Name;
                    entity.Timestamp = DateTime.Now;
                    entity.PerformedBy = Environment.UserName;
                    entity.ActionType = entry.Action.ToString();

                    // If the primary key is a composite key, we can only take the first value.
                    entity.RecordId = entry.PrimaryKey.First().Value.ToString();
                })
                .IgnoreMatchedProperties(true)

            )))
            .WithCreationPolicy(EventCreationPolicy.InsertOnEnd);



        Audit.EntityFramework.Configuration.Setup()
            .ForAnyContext()
            .UseOptOut()
            .Ignore<Domain.Logs.AuditLog>()
            .Ignore<Domain.Logs.ApiLog>();

    }
    /// <summary>
    /// Setups the audit output
    /// </summary>
    public static IServiceCollection AddAuditDataProvider(this IServiceCollection services)
    {
        Audit.Core.Configuration.JsonSettings.WriteIndented = true;

        services.AddSingleton<AuditDataProvider>(new FileDataProvider(cfg => cfg
            .Directory(@"D:\logs")
            .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}.json")));

        return services;
    }

    /// <summary>
    /// Add the global audit filter to the MVC pipeline
    /// </summary>
    public static MvcOptions AuditSetupMvcFilter(this MvcOptions mvcOptions)
    {
        // Add the global MVC Action Filter to the filter chain
        mvcOptions.AddAuditFilter(a => a
            .LogAllActions()
            .WithEventType("MVC")
            .IncludeModelState()
            .IncludeRequestBody()
            .IncludeResponseBody());

        return mvcOptions;
    }

    public static IApplicationBuilder UseAuditTrail(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        app.Use(async (context, next) => {  
            context.Request.EnableBuffering(); // or .EnableRewind();
            await next();
        });

        // Use the audit middleware.
        return app.UseAuditMiddleware(_ => _
             .FilterByRequest(rq => !rq.Path.Value.EndsWith("favicon.ico"))
            .WithEventType("HTTP:{verb}:{url}")
            .IncludeHeaders()
            .IncludeResponseHeaders()
            .IncludeRequestBody()
            .IncludeResponseBody());
    }
}
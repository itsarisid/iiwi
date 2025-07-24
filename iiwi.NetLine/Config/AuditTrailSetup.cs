using Audit.Core;
using Audit.Core.Providers;
using Audit.EntityFramework.Providers;
using Audit.WebApi;
using iiwi.Common;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iiwi.NetLine.Config;

/// <summary>
/// Provides extension methods for configuring audit trail functionality
/// </summary>
/// <remarks>
/// This static class configures comprehensive auditing for:
/// - Entity Framework data changes
/// - MVC controller actions
/// - HTTP request/response traffic
/// 
/// Supports multiple output formats including:
/// - JSON files for HTTP traffic
/// - Database storage for EF changes
/// </remarks>
public static class AuditTrailSetup
{
    /// <summary>
    /// Configures the core audit trail infrastructure
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <param name="configuration">Application configuration</param>
    /// <remarks>
    /// <para>
    /// This setup performs the following configurations:
    /// 1. Configures audit logging directory (from config or default location)
    /// 2. Sets up JSON serialization settings for audit logs
    /// 3. Configures Entity Framework auditing:
    ///    - Special handling for sensitive fields (PasswordHash)
    ///    - Custom type mappings for audit entities
    ///    - Exclusion rules for history tables
    /// 4. Sets conditional audit providers:
    ///    - File logs for HTTP traffic
    ///    - Database storage for EF changes
    /// </para>
    /// <para>
    /// Audit events include:
    /// - Activity traces
    /// - Stack traces
    /// - Full change data
    /// </para>
    /// </remarks>
    public static void AddAuditTrail(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Configure audit log directory
        var auditLogDirectory = configuration.GetValue<string>("AuditLog:Directory")
                           ?? Path.Combine(Environment.CurrentDirectory, General.Directories.Logs, General.Directories.Audit);

        // Configure JSON serialization for audit logs
        Configuration.JsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true
        };

        // Configure Entity Framework auditing
        Audit.EntityFramework.Configuration.Setup()
               .ForContext<ApplicationDbContext>(config => config
               .ForEntity<ApplicationUser>(_ => _
                .Ignore(user => user.ConcurrencyStamp)
                .Override(user => user.PasswordHash, null) // Redact sensitive data
                .Format(user => user.Gender, pass => new String('*', pass.Length)))
               .IncludeEntityObjects()
               .AuditEventType("{context}:{database}"))
               .UseOptOut()
                   .IgnoreAny(t => t.Name.EndsWith("History"));

        // Configure audit pipeline
        Configuration.Setup()
            .AuditDisabled(false)
            .IncludeActivityTrace()
            .IncludeStackTrace()
            .UseConditional(c => c
                .When(ev => ev.EventType.StartsWith("HTTP"), new FileDataProvider(cfg => cfg
                    .Directory(auditLogDirectory)
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
                    entity.RecordId = entry.PrimaryKey.First().Value.ToString();
                })
                .IgnoreMatchedProperties(true)
            )))
            .WithCreationPolicy(EventCreationPolicy.InsertOnEnd);

        // Configure global EF audit exclusions
        Audit.EntityFramework.Configuration.Setup()
            .ForAnyContext()
            .UseOptOut()
            .Ignore<Domain.Logs.AuditLog>()
            .Ignore<Domain.Logs.ApiLog>();
    }

    /// <summary>
    /// Configures the audit data provider service
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection</returns>
    /// <remarks>
    /// Registers a file-based audit data provider that:
    /// - Stores logs in JSON format
    /// - Uses pretty-printed formatting
    /// - Saves to {approot}/Logs/Audit directory
    /// - Names files with timestamp precision to milliseconds
    /// </remarks>
    public static IServiceCollection AddAuditDataProvider(this IServiceCollection services, IConfiguration configuration)
    {
        Audit.Core.Configuration.JsonSettings.WriteIndented = true;

        var auditLogDirectory = configuration.GetValue<string>("AuditLog:Directory")
                               ?? Path.Combine(Environment.CurrentDirectory, General.Directories.Logs, General.Directories.Audit);

        services.AddSingleton<AuditDataProvider>(new FileDataProvider(cfg => cfg
            .Directory(auditLogDirectory)
            .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}.json")));

        return services;
    }
           

    /// <summary>
    /// Configures MVC action auditing
    /// </summary>
    /// <param name="mvcOptions">The MVC options to configure</param>
    /// <returns>The configured MVC options</returns>
    /// <remarks>
    /// Adds global MVC action filters that audit:
    /// - All controller actions
    /// - Request/response bodies
    /// - Model state
    /// - Uses "MVC" event type prefix
    /// </remarks>
    public static MvcOptions AuditSetupMvcFilter(this MvcOptions mvcOptions)
    {
        mvcOptions.AddAuditFilter(a => a
            .LogAllActions()
            .WithEventType("MVC")
            .IncludeModelState()
            .IncludeRequestBody()
            .IncludeResponseBody());

        return mvcOptions;
    }

    /// <summary>
    /// Enables HTTP request/response auditing middleware
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <returns>The configured application builder</returns>
    /// <remarks>
    /// Configures middleware that:
    /// - Buffers requests for body auditing
    /// - Filters out favicon requests
    /// - Audits headers and bodies
    /// - Uses "HTTP:{verb}:{url}" event type format
    /// </remarks>
    public static IApplicationBuilder UseAuditTrail(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });

        return app.UseAuditMiddleware(_ => _
             .FilterByRequest(rq => !rq.Path.Value.EndsWith("favicon.ico"))
            .WithEventType("HTTP:{verb}:{url}")
            .IncludeHeaders()
            .IncludeResponseHeaders()
            .IncludeRequestBody()
            .IncludeResponseBody());
    }
}
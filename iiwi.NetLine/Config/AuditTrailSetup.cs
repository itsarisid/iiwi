using Audit.Core;
using Audit.Core.Providers;
using Audit.EntityFramework.Providers;
using Audit.WebApi;
using iiwi.Database;
using iiwi.Domain.Identity;
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


        Configuration.Setup()
            .AuditDisabled(false) // Enable auditing
            .IncludeActivityTrace()
            .IncludeStackTrace()
            .UseConditional(c => c
                .When(ev => ev.EventType == "API", new FileDataProvider(file => file.Directory(@"D:\logs")))
                .Otherwise(new EntityFrameworkDataProvider(_ => _
                .AuditTypeMapper(t => typeof(Domain.Logs.AuditLog))
                .AuditEntityAction<Domain.Logs.AuditLog>((ev, entry, entity) =>
                {
                    entity.AuditData = entry.ToJson();
                    entity.EntityType = entry.EntityType.Name;
                    entity.AuditDate = DateTime.Now;
                    entity.AuditUser = Environment.UserName;
                    //entity.AuditUser = Environment.MachineName;
                    //entity.IPAddress = Environment.IpAddress;
                    entity.AuditAction = entry.Action.ToString();
                    // If the primary key is a composite key, we can only take the first value.
                    entity.TablePk = entry.PrimaryKey.First().Value.ToString();
                })
                .IgnoreMatchedProperties(true)

            )))
            .WithCreationPolicy(EventCreationPolicy.InsertOnEnd);

        // Configure the Entity framework audit.
        //_ = Configuration.Setup()
        //.AuditDisabled(false) // Enable auditing
        //.IncludeActivityTrace()
        //.IncludeStackTrace()
        //.UseEntityFramework(_ => _
        //    .AuditTypeMapper(t => typeof(Domain.Logs.AuditLog))
        //    .AuditEntityAction<Domain.Logs.AuditLog>((ev, entry, entity) =>
        //    {
        //        entity.AuditData = entry.ToJson();
        //        entity.EntityType = entry.EntityType.Name;
        //        entity.AuditDate = DateTime.Now;
        //        entity.AuditUser = Environment.UserName;
        //        //entity.AuditUser = Environment.MachineName;
        //        //entity.IPAddress = Environment.IpAddress;
        //        entity.AuditAction = entry.Action.ToString();
        //        // If the primary key is a composite key, we can only take the first value.
        //        entity.TablePk = entry.PrimaryKey.First().Value.ToString();
        //    })// Ignore these properties in the audit log
        //.IgnoreMatchedProperties(true))
        //.WithCreationPolicy(EventCreationPolicy.InsertOnEnd); // Insert the audit log at the end of the transaction

        Configuration.JsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true
        };

        //Audit.EntityFramework.Configuration.Setup()
        //    .ForAnyContext()
        //    .UseOptOut()
        //    .Ignore<Domain.Logs.AuditLog>()
        //    .Ignore<Domain.Logs.ApiLog>();

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
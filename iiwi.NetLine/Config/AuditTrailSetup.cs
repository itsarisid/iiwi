using Audit.Core;
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

        // Configure the Entity framework audit.
        Configuration.Setup()
        .IncludeActivityTrace()
        .IncludeStackTrace()
        .UseEntityFramework(_ => _
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
        .IgnoreMatchedProperties(true))
        .WithCreationPolicy(EventCreationPolicy.InsertOnEnd); // Insert the audit log at the end of the transaction

        Configuration.JsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true
        };
    }
}

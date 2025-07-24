using Audit.Core;

namespace iiwi.NetLine.AuditLog;

/// <summary>
/// Custom Audit Scope Factory that enriches audit events with HTTP context information
/// </summary>
/// <remarks>
/// This factory extends the base AuditScopeFactory to automatically include:
/// - HTTP request correlation identifiers
/// - User authentication context
/// 
/// The enriched data helps with:
/// - Tracing requests across services
/// - Auditing user actions
/// - Correlating logs with audit events
/// </remarks>
/// <param name="_httpContextAccessor">Provides access to the current HTTP context</param>
public class IiwiAuditScopeFactory(IHttpContextAccessor _httpContextAccessor) : AuditScopeFactory
{
    /// <summary>
    /// Configures options for the audit scope
    /// </summary>
    /// <param name="options">The audit scope configuration options</param>
    /// <remarks>
    /// Currently performs null validation but can be extended to:
    /// - Apply custom event filtering
    /// - Configure data providers
    /// - Set default event values
    /// </remarks>
    public override void OnConfiguring(AuditScopeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
    }

    /// <summary>
    /// Enriches audit events when a new scope is created
    /// </summary>
    /// <param name="auditScope">The newly created audit scope</param>
    /// <remarks>
    /// Automatically adds the following HTTP context information:
    /// - Trace Identifier: For correlating with request logs
    /// - Username: The authenticated user (if available)
    /// 
    /// The fields are added as custom fields to the audit event and will be
    /// included in the audit output (database, logs, etc.)
    /// </remarks>
    public override void OnScopeCreated(AuditScope auditScope)
    {
        auditScope.SetCustomField("TraceId", _httpContextAccessor.HttpContext?.TraceIdentifier);
        auditScope.SetCustomField("UserName", _httpContextAccessor.HttpContext?.User.Identity?.Name);
    }
}

using Audit.Core;

namespace iiwi.NetLine.AuditLog;

/// <summary>
/// Custom Audit Scope Factory that includes information to the audit events from the HttpContext
/// </summary>
public class IiwiAuditScopeFactory(IHttpContextAccessor _httpContextAccessor) : AuditScopeFactory
{
    /// <inheritdoc />
    public override void OnConfiguring(AuditScopeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
    }

    /// <inheritdoc />
    public override void OnScopeCreated(AuditScope auditScope)
    {
        auditScope.SetCustomField("TraceId", _httpContextAccessor.HttpContext?.TraceIdentifier);
        auditScope.SetCustomField("UserName", _httpContextAccessor.HttpContext?.User.Identity?.Name);
    }

}
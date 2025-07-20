
using Audit.EntityFramework;
using DotNetCore.Domain;

namespace iiwi.Domain.Logs;


[AuditIgnore]
public class AuditLog: Entity
{
    public string AuditData { get; set; }
    public string TablePk { get; set; }
    public string EntityType { get; set; }

    public DateTime AuditDate { get; set; }
    public string AuditAction { get; set; }
    public string AuditUser { get; set; }
    //public string AuditUserIP { get; set; }
}

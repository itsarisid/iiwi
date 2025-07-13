using DotNetCore.Domain;
using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class RolePermission: Entity
{
    public int? RoleId { get; set; }

    public long PermissionId { get; set; }

    public virtual Permission Permission { get; set; }

    public virtual ApplicationRole Role { get; set; }
}

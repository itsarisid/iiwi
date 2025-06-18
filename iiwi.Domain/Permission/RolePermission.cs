using DotNetCore.Domain;
using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class RolePermission: Entity
{
    public long PermissionId { get; set; }
    public long RoleId { get; set; }
    public ApplicationRole Role { get; set; }
    public Permission Permission { get; set; } = null!;
}

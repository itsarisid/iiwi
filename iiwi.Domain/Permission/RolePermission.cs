using DotNetCore.Domain;
using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class RolePermission: Entity
{
    public ApplicationRole Role { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}

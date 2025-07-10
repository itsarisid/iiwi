using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class Permission: BaseEntity
{
    public string CodeName { get; set; }
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
}

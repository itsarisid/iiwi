using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class Permission: BaseEntity
{
    public string Name { get; set; }
    public string CodeName { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<RolePermission> RolePermissions { get; set; }
}

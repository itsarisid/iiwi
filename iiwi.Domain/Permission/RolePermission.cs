using iiwi.Domain.Identity;

namespace iiwi.Domain;

public class RolePermission:BaseEntity
{
    public ApplicationRole Role { get; set; }
    public Permission Permissions { get; set; }
}

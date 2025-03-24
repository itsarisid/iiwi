
using Microsoft.AspNetCore.Identity;

namespace iiwi.Domain.Identity;

public class ApplicationRole: IdentityRole<int>
{
    public ICollection<RolePermission> RolePermissions { get; set; }
}

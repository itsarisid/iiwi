
using Microsoft.AspNetCore.Identity;

namespace iiwi.Domain.Identity;

public class ApplicationRole: IdentityRole<int>
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}

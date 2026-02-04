
using Microsoft.AspNetCore.Identity;

namespace iiwi.Domain.Identity;

public class ApplicationRole: IdentityRole<int>
{
    /// <summary>
    /// Gets or sets the UserRoles.
    /// </summary>
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}

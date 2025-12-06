
using Microsoft.AspNetCore.Identity;

namespace iiwi.Domain.Identity;

/// <summary>
/// Represents an application role.
/// </summary>
public class ApplicationRole: IdentityRole<int>
{
    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}

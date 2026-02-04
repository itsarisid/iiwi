using Microsoft.AspNetCore.Identity;
using System.Data;

namespace iiwi.Domain.Identity;

public class ApplicationUserRole: IdentityUserRole<int>
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the User.
    /// </summary>
    public virtual ApplicationUser User { get; set; } = null!;
    /// <summary>
    /// Gets or sets the Role.
    /// </summary>
    public virtual ApplicationRole Role { get; set; } = null!;
}

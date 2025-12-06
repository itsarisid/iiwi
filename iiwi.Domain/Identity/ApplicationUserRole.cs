using Microsoft.AspNetCore.Identity;


namespace iiwi.Domain.Identity;

/// <summary>
/// Represents an application user role.
/// </summary>
public class ApplicationUserRole: IdentityUserRole<int>
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public virtual ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public virtual ApplicationRole Role { get; set; } = null!;
}

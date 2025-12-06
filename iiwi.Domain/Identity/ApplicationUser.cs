using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace iiwi.Domain.Identity;

/// <summary>
/// Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser<int>
{
    /// <summary>
    /// Gets or sets the gender.
    /// </summary>
    [PersonalData]
    public string Gender { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [PersonalData]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [PersonalData]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    [PersonalData]
    public DateTime DOB { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the last login date.
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }=[];
}

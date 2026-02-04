using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace iiwi.Domain.Identity;

public class ApplicationUser : IdentityUser<int>
{
    /// <summary>
    /// Gets or sets the Gender.
    /// </summary>
    [PersonalData]
    public string Gender { get; set; }
    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    [PersonalData]
    public string FirstName { get; set; }
    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    [PersonalData]
    public string LastName { get; set; }
    /// <summary>
    /// Gets or sets the DisplayName.
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// Gets or sets the DOB.
    /// </summary>
    [PersonalData]
    public DateTime DOB { get; set; }
    /// <summary>
    /// Gets or sets the Address.
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// Gets or sets the LastLogin.
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the UserRoles.
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }=[];
}

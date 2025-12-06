using System.ComponentModel.DataAnnotations;

namespace iiwi.Application;

/// <summary>
/// Request model for updating user profile.
/// </summary>
public record UpdateProfileRequest
{
    /// <summary>
    /// Gets or sets the gender.
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    [Display(Name = "Date of Birth")]
    public DateTime DOB { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    [Display(Name = "Address")]
    public string Address { get; set; }
}
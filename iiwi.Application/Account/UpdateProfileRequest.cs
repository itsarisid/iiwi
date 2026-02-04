using System.ComponentModel.DataAnnotations;

namespace iiwi.Application;

public record UpdateProfileRequest
{
    /// <summary>
    /// Gets or sets the Gender.
    /// </summary>
    public string Gender { get; set; }
    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    /// <summary>
    /// Gets or sets the DisplayName.
    /// </summary>
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; }
    /// <summary>
    /// Gets or sets the DOB.
    /// </summary>
    [Display(Name = "Date of Birth")]
    public DateTime DOB { get; set; }
    /// <summary>
    /// Gets or sets the Address.
    /// </summary>
    [Display(Name = "Address")]
    public string Address { get; set; }
}

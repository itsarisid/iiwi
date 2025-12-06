
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for login.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to remember the user.
    /// </summary>
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}

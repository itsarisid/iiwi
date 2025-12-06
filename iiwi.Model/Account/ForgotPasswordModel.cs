
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for forgot password.
/// </summary>
public class ForgotPasswordModel
{
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

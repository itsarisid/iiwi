using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for resetting the password.
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets the reset code.
    /// </summary>
    [Required]
    public string Code { get; set; }
}

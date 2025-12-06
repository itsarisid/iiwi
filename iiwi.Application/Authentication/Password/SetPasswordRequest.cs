using System.ComponentModel.DataAnnotations;
namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for setting the password.
/// </summary>
public record SetPasswordRequest
{
    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

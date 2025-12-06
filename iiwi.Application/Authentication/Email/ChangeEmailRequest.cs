using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for changing the user's email address.
/// </summary>
public record ChangeEmailRequest
{
    /// <summary>
    /// Gets or sets the new email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}

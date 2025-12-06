using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for confirming email change.
/// </summary>
public record ConfirmEmailChangeRequest
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the new email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the confirmation code.
    /// </summary>
    [Required]
    public string Code { get; set; }
}

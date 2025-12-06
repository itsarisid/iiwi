using System.ComponentModel.DataAnnotations;
namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for confirming email.
/// </summary>
public class ConfirmEmailRequest
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the confirmation code.
    /// </summary>
    [Required]
    public string Code { get; set; }
}

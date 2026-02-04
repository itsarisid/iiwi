using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record ConfirmEmailChangeRequest
{
    /// <summary>
    /// Gets or sets the UserId.
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the Code.
    /// </summary>
    [Required]
    public string Code { get; set; }
}

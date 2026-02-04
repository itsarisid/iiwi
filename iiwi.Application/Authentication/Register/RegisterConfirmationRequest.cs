using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record RegisterConfirmationRequest
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

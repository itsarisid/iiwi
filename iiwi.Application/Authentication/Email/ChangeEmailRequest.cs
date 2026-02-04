using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record ChangeEmailRequest
{
    /// <summary>
    /// Gets or sets the NewEmail.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}

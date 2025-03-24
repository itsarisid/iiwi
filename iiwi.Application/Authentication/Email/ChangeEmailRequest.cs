using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record ChangeEmailRequest
{
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}

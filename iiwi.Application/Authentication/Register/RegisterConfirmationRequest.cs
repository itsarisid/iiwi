using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record RegisterConfirmationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

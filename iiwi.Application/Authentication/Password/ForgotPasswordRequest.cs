using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record ConfirmEmailChangeRequest
{
    [Required]
    public string UserId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Code { get; set; }
}

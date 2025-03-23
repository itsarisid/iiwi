using System.ComponentModel.DataAnnotations;

namespace Architecture.Application.Authentication
{
    public record ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

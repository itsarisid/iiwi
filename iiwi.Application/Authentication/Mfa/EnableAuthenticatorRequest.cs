using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record EnableAuthenticatorRequest
{
    /// <summary>
    /// Gets or sets the Code.
    /// </summary>
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Verification Code")]
    public string Code { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LoginWith2faRequest
{
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Authenticator code")]
    public string TwoFactorCode { get; set; }

    [Display(Name = "Remember this machine")]
    public bool RememberMachine { get; set; }
}

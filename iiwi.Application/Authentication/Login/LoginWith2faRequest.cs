using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LoginWith2faRequest
{
    /// <summary>
    /// Gets or sets the TwoFactorCode.
    /// </summary>
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Authenticator code")]
    public string TwoFactorCode { get; set; }

    /// <summary>
    /// Gets or sets the RememberMachine.
    /// </summary>
    [Display(Name = "Remember this machine")]
    public bool RememberMachine { get; set; }
}

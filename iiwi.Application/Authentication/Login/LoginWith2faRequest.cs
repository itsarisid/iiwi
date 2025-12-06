using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for two-factor authentication login.
/// </summary>
public record LoginWith2faRequest
{
    /// <summary>
    /// Gets or sets the authenticator code.
    /// </summary>
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Authenticator code")]
    public string TwoFactorCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to remember this machine.
    /// </summary>
    [Display(Name = "Remember this machine")]
    public bool RememberMachine { get; set; }
}

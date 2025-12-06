using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for login with 2FA.
/// </summary>
public class LoginWith2faModel
{
    /// <summary>
    /// Gets or sets the two-factor code.
    /// </summary>
    [Required]
    [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Authenticator code")]
    public string TwoFactorCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to remember the machine.
    /// </summary>
    [Display(Name = "Remember this machine")]
    public bool RememberMachine { get; set; }
}
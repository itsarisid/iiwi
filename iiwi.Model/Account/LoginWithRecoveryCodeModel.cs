using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for login with recovery code.
/// </summary>
public class LoginWithRecoveryCodeModel
{
    /// <summary>
    /// Gets or sets the recovery code.
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}
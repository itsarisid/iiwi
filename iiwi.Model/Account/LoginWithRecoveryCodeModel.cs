

using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class LoginWithRecoveryCodeModel
{
    /// <summary>
    /// Gets or sets the RecoveryCode.
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}

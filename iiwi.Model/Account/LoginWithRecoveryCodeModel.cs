

using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class LoginWithRecoveryCodeModel
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}
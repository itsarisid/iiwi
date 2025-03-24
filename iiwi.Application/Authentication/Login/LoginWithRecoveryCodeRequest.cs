
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LoginWithRecoveryCodeRequest
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}

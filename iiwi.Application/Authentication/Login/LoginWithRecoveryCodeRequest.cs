
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LoginWithRecoveryCodeRequest
{
    /// <summary>
    /// Gets or sets the RecoveryCode.
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}

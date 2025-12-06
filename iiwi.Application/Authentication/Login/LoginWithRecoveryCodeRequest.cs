
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for login with recovery code.
/// </summary>
public record LoginWithRecoveryCodeRequest
{
    /// <summary>
    /// Gets or sets the recovery code.
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Recovery Code")]
    public string RecoveryCode { get; set; }
}

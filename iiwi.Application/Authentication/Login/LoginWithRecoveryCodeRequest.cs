
using System.ComponentModel.DataAnnotations;

namespace Architecture.Application.Authentication
{
    public record LoginWithRecoveryCodeRequest
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}

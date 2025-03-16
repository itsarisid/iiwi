
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

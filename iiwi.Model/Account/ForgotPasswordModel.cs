
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class ForgotPasswordModel
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

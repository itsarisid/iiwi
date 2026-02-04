
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class LoginModel
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the RememberMe.
    /// </summary>
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}

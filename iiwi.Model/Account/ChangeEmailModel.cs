using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class ChangeEmailModel
{
    /// <summary>
    /// Gets or sets the NewEmail.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}


using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for changing email.
/// </summary>
public class ChangeEmailModel
{
    /// <summary>
    /// Gets or sets the new email.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}


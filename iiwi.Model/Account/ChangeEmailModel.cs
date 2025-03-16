using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class ChangeEmailModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}


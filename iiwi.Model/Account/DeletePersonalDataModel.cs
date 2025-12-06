
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

/// <summary>
/// Model for deleting personal data.
/// </summary>
public class DeletePersonalDataModel
{
    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}


using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class DeletePersonalDataModel
{
    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

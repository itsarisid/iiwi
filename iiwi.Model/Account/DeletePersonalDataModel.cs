
using System.ComponentModel.DataAnnotations;

namespace iiwi.Model.Account;

public class DeletePersonalDataModel
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

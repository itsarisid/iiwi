
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record DeletePersonalDataRequest
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

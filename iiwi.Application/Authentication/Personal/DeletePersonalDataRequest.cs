
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record DeletePersonalDataRequest
{
    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

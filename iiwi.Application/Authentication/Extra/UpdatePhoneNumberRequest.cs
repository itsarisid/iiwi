using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record UpdatePhoneNumberRequest
{
    /// <summary>
    /// Gets or sets the PhoneNumber.
    /// </summary>
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}

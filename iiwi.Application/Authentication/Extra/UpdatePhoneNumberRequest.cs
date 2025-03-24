using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record UpdatePhoneNumberRequest
{
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for updating phone number.
/// </summary>
public record UpdatePhoneNumberRequest
{
    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}

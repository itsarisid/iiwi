using System.ComponentModel.DataAnnotations;
namespace iiwi.Application.Authentication;

public class ConfirmEmailRequest
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Code { get; set; }
}

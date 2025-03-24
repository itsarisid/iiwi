using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LinkLoginRequest
{
    [Required]
    public string Provider { get; set; }
}

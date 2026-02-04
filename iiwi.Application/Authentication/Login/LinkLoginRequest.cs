using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

public record LinkLoginRequest
{
    /// <summary>
    /// Gets or sets the Provider.
    /// </summary>
    [Required]
    public string Provider { get; set; }
}

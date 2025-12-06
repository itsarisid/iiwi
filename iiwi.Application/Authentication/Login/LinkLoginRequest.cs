using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for linking an external login provider.
/// </summary>
public record LinkLoginRequest
{
    /// <summary>
    /// Gets or sets the provider name.
    /// </summary>
    [Required]
    public string Provider { get; set; }
}

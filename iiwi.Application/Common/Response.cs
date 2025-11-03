/// <summary>
/// Namespace: iiwi.Application. Common response type used across application handlers.
/// </summary>
namespace iiwi.Application;

/// <summary>
/// Represents a simple response containing a message.
/// </summary>
public record Response
{
    /// <summary>
    /// Gets or sets the response message.
    /// </summary>
    public string Message { get; set; }
}
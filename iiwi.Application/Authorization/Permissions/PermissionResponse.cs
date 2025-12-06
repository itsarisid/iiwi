
namespace iiwi.Application.Authorization;

/// <summary>
/// Response model for permissions.
/// </summary>
public record PermissionResponse : Response
{
    /// <summary>
    /// Gets or sets the permissions.
    /// </summary>
    public IEnumerable<string> Permissions { get; set; }
}

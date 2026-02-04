
namespace iiwi.Application.Authorization;

public record PermissionResponse : Response
{
    /// <summary>
    /// Gets or sets the Permissions.
    /// </summary>
    public IEnumerable<string> Permissions { get; set; }
}

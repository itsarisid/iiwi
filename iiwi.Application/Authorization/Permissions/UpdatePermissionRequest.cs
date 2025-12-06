namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for updating permissions.
/// </summary>
public class UpdatePermissionRequest
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the permissions.
    /// </summary>
    public List<int> Permissions { get; set; }
    
}

namespace iiwi.Application.Authorization;

public class UpdatePermissionRequest
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Gets or sets the Permissions.
    /// </summary>
    public List<int> Permissions { get; set; }
    
}

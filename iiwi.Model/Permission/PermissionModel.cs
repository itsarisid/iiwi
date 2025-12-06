namespace iiwi.Model.Permission;

/// <summary>
/// Represents a permission model.
/// </summary>
public class PermissionModel
{
    /// <summary>
    /// Gets the ID.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public required string Name { get; init; }
}
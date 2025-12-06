namespace iiwi.Domain;

/// <summary>
/// Represents a permission.
/// </summary>
public class Permission: BaseEntity
{
    /// <summary>
    /// Gets or sets the code name.
    /// </summary>
    public string CodeName { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

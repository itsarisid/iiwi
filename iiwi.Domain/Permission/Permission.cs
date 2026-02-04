namespace iiwi.Domain;

public class Permission: BaseEntity
{
    /// <summary>
    /// Gets or sets the CodeName.
    /// </summary>
    public string CodeName { get; set; }
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

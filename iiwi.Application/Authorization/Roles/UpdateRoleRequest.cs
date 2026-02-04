using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class UpdateRoleRequest
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [JsonIgnore]
    public  int Id { get; set; }
    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string Description { get; set; }
}

public class UpdateRoleParams
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public int Id { get; set; }
}

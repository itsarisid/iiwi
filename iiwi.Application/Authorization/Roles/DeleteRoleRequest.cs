using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class DeleteRoleRequest
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; } 
}
public class DeleteRoleParams
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public int Id { get; set; }
}

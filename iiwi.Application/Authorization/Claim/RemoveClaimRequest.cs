using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class RemoveClaimRequest
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the RoleId.
    /// </summary>
    [JsonIgnore]
    public int RoleId { get; set; }
}
public class RemoveClaimParams
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the RoleId.
    /// </summary>
    public int RoleId { get; set; }
}

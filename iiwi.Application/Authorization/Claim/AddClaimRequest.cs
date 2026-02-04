
using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class AddClaimRequest
{
    /// <summary>
    /// Gets or sets the RoleId.
    /// </summary>
    [JsonIgnore]
    public int RoleId { get; set; }
}
public class AddClaimParams
{
    /// <summary>
    /// Gets or sets the RoleId.
    /// </summary>
    public int RoleId { get; set; }
}

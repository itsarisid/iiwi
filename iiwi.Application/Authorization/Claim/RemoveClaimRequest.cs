using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class RemoveClaimRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public int RoleId { get; set; }
}
public class RemoveClaimParams
{
    public int Id { get; set; }
    public int RoleId { get; set; }
}

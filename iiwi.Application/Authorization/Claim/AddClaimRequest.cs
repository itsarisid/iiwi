
using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class AddClaimRequest
{
    [JsonIgnore]
    public int RoleId { get; set; }
}
public class AddClaimParams
{
    public int RoleId { get; set; }
}

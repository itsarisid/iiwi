using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class GetRoleClaimsRequest
{
    [JsonIgnore]
    public int RoleId { get; set; }
}
public class GetRoleClaimsParams
{
    public int RoleId { get; set; }
}

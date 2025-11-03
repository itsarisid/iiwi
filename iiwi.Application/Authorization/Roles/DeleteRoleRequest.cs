using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class DeleteRoleRequest
{
    [JsonIgnore]
    public int Id { get; set; } 
}
public class DeleteRoleParams
{
    public int Id { get; set; }
}
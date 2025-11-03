using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

public class UpdateRoleRequest
{
    [JsonIgnore]
    public  int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateRoleParams
{
    public int Id { get; set; }
}

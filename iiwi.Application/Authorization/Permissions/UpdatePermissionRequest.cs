namespace iiwi.Application.Authorization;

public class UpdatePermissionRequest
{
    public string Id { get; set; }
    public List<int> Permissions { get; set; }
    
}

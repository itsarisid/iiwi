
namespace iiwi.Application.Authorization;

public record PermissionResponse : Response
{
    public IEnumerable<string> Permissions { get; set; }
}

using static iiwi.Library.Helper;

namespace iiwi.Application.Authorization;

public class UpdateRoleRequest
{
    private  int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateRoleParams
{
    public int Id { get; set; }
}

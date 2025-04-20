using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iiwi.NetLine.Filters;

public enum PermissionItem
{
    User,
    Product,
    Contact,
    Review,
    Client
}

public enum PermissionAction
{
    Read,
    Create,
}


public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(PermissionItem item, PermissionAction action)
    : base(typeof(AuthorizeActionFilter))
    {
        Arguments = new object[] { item, action };
    }
}

public class AuthorizeActionFilter(PermissionItem _item, PermissionAction _action) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _item, _action); // :)

        if (!isAuthorized)
        {
            context.Result = new ForbidResult();
        }
    }

    private bool MumboJumboFunction(ClaimsPrincipal user, PermissionItem item, PermissionAction action)
    {
        return true;
    }
}

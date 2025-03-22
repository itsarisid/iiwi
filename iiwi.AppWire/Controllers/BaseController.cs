using DotNetCore.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.AppWire;

[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>Gets the mediator.</summary>
    /// <value>The mediator.</value>
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}

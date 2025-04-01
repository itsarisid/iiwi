using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

public class LogoutHandler(
    SignInManager<ApplicationUser> _signInManager
    ) : IHandler<LogoutRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.LogoutRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(LogoutRequest request)
    {
        await _signInManager.SignOutAsync();

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Logout successfully"
        });
    }
}

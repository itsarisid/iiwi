using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for processing user logout.
/// </summary>
/// <param name="_signInManager">The sign-in manager.</param>
public class LogoutHandler(
    SignInManager<ApplicationUser> _signInManager
    ) : IHandler<LogoutRequest, Response>
{

    /// <summary>
    /// Handles the logout request asynchronously.
    /// </summary>
    /// <param name="request">The logout request.</param>
    /// <summary>
    /// Signs out the current authenticated user and returns a success response.
    /// </summary>
    /// <returns>A Result containing a Response with HTTP status 200 (OK) and Message = "Logout successfully".</returns>
    public async Task<Result<Response>> HandleAsync(LogoutRequest request)
    {
        await _signInManager.SignOutAsync();

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Logout successfully"
        });
    }
}
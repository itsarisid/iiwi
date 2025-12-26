using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication.Login;

public class RemoveLoginHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider) : IHandler<RemoveLoginRequest, Response>
{
    /// <summary>
    /// Removes an external login from the current authenticated user and refreshes the user's sign-in.
    /// </summary>
    /// <param name="request">Request containing the external login provider name and provider key to remove.</param>
    /// <returns>
    /// A Result containing a Response with a message and an HTTP status code:
    /// 200 OK when the external login was removed,
    /// 400 Bad Request if the removal failed,
    /// 404 Not Found if the current user could not be loaded.
    /// <summary>
    /// Removes an external login from the currently authenticated user and refreshes the user's sign-in.
    /// </summary>
    /// <param name="request">The request containing the external login provider name and provider key to remove.</param>
    /// <returns>A Result&lt;Response&gt; with status code 200 when the external login was removed, 400 if removal failed, or 404 if the current user cannot be found; the Response contains a descriptive message.</returns>
    public async Task<Result<Response>> HandleAsync(RemoveLoginRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var result = await _userManager.RemoveLoginAsync(user, request.LoginProvider, request.ProviderKey);
        if (!result.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = "The external login was not removed."
            });
        }

        await _signInManager.RefreshSignInAsync(user);
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "The external login was removed.",
        });
    }
}
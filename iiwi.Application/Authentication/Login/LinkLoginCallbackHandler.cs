using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication
{
    /// <summary>
    /// Handler for processing the callback from linking an external login.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_signInManager">The sign-in manager.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class LinkLoginCallbackHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<LinkLoginCallbackHandler> _logger) : IHandler<LinkLoginCallbackRequest, Response>
    {

        /// <summary>
        /// Handles the link login callback request asynchronously.
        /// </summary>
        /// <param name="request">The link login callback request.</param>
        /// <summary>
        /// Handles the external login callback and associates the external login with the current user account.
        /// </summary>
        /// <returns>A Result containing a Response. On success the result has HTTP 200 with Message "The external login was added."; on failure the result has HTTP 400 with an explanatory message (user not found or external login not added).</returns>
        /// <summary>
        /// Associates the external login returned by the authentication callback with the current user account.
        /// </summary>
        /// <param name="request">The link login callback request.</param>
        /// <returns>A Result&lt;Response&gt; containing HTTP 200 with the message "The external login was added." on success, or HTTP 400 with an explanatory message on failure.</returns>
        /// <exception cref="InvalidOperationException">Thrown when external login information cannot be loaded for the user.</exception>
        public async Task<Result<Response>> HandleAsync(LinkLoginCallbackRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user)) ?? throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = "The external login was not added. External logins can only be associated with one account."
                });
            }



            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "The external login was added.",
            });
        }
    }
}
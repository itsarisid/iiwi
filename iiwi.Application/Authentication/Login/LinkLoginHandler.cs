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
    /// Handler for linking external logins.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_signInManager">The sign-in manager.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class LinkLoginHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<LinkLoginHandler> _logger) : IHandler<LinkLoginRequest, Response>
    {

        /// <summary>
        /// Handles the link login request asynchronously.
        /// </summary>
        /// <param name="request">The link login request.</param>
        /// <summary>
        /// Handles a link-login request by resolving the current user and returning an appropriate response.
        /// </summary>
        /// <param name="request">The link login request containing external login data.</param>
        /// <summary>
        /// Handles a link-login request by resolving the current user and producing an HTTP response indicating the outcome.
        /// </summary>
        /// <param name="request">The link-login request details.</param>
        /// <returns>A Result containing a Response: `400 Bad Request` with an explanatory message if the current user cannot be loaded; otherwise `200 OK` with a placeholder message indicating the method is not implemented.</returns>
        public async Task<Result<Response>> HandleAsync(LinkLoginRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }





            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Method not implemented",
            });
        }
    }
}
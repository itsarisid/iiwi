using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class LinkLoginHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<LinkLoginHandler> _logger) : IHandler<LinkLoginRequest, Response>
    {
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

            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            //var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            //var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl, _userManager.GetUserId(_claimsProvider.ClaimsPrinciple));
            //return Ok(new ChallengeResult(request.Provider, properties));

            // Clear the existing external cookie to ensure a clean login process

            //FIXME: Need to find out how to call this method
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //StatusMessage = "The external login was added.";
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Method not implemented",
            });
        }
    }
}

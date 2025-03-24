using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class LinkLoginCallbackHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<LinkLoginCallbackHandler> logger) : IHandler<LinkLoginCallbackRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly ILogger<LinkLoginCallbackHandler> _logger = logger;

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

            // Clear the existing external cookie to ensure a clean login process

            //FIXME: Need to find out how to call this method
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //StatusMessage = "The external login was added.";
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "The external login was added.",
            });
        }
    }
}


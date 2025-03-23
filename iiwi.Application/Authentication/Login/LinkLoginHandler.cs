using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class LinkLoginHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<LinkLoginHandler> logger,
    IResultService resultService) : IHandler<LinkLoginRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LinkLoginHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(LinkLoginRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
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
            return _resultService.Success(new Response
            {
                Message = "Method not implemented",
            });
        }
    }
}

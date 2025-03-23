using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Architecture.Application.Authentication
{
    public class LinkLoginCallbackHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<LinkLoginCallbackHandler> logger,
    IResultService resultService) : IHandler<LinkLoginCallbackRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LinkLoginCallbackHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(LinkLoginCallbackRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user)) ?? throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                return _resultService.Error<Response>("The external login was not added. External logins can only be associated with one account.");
            }

            // Clear the existing external cookie to ensure a clean login process

            //FIXME: Need to find out how to call this method
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //StatusMessage = "The external login was added.";
            return _resultService.Success(new Response
            {
                Message = "The external login was added.",
            });
        }
    }
}


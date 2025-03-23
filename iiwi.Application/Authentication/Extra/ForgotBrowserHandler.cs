using Microsoft.AspNetCore.Identity; 

namespace Architecture.Application.Authentication
{
    public class ForgotBrowserHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    IResultService resultService) : IHandler<ForgotBrowserRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(ForgotBrowserRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            await _signInManager.ForgetTwoFactorClientAsync();
            return _resultService.Success(new Response
            {
                Message = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code."
            });
        }
    }
}


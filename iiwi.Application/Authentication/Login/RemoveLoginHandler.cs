using Microsoft.AspNetCore.Identity;

namespace Architecture.Application.Authentication
{
    public class RemoveLoginHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    IResultService resultService) : IHandler<RemoveLoginRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IResultService _resultService = resultService;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;

        public async Task<Result<Response>> HandleAsync(RemoveLoginRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, request.LoginProvider, request.ProviderKey);
            if (!result.Succeeded)
            {
                return _resultService.Error<Response>("The external login was not removed.");
            }

            await _signInManager.RefreshSignInAsync(user);
            return _resultService.Success<Response>(new Response
            {
                Message = "The external login was removed.",
            });
        }
    }
}


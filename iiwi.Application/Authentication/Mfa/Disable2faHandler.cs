using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Architecture.Application.Authentication
{
    public class Disable2faHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    ILogger<Disable2faHandler> logger,
    IResultService resultService) : IHandler<Disable2faRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<Disable2faHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(Disable2faRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(_claimsProvider.ClaimsPrinciple));
            return _resultService.Success(new Response
            {
                Message = "2fa has been disabled. You can re enable 2fa when you setup an authenticator app and call TwoFactorAuthentication",
            });
        }
    }
}

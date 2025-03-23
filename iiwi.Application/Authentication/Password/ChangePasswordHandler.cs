using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace Architecture.Application.Authentication
{
    public class ChangePasswordHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<ChangePasswordHandler> logger,
    IResultService resultService) : IHandler<ChangePasswordRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider= claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<ChangePasswordHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                _logger.LogError("Error while changing password");
                return _resultService.Error<Response>("Error while changing password");
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            return _resultService.Success(new Response
            {
                Message = "Your password has been changed.",
            });
        }
    }
}


using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Architecture.Application.Authentication
{
    public class LoginWithRecoveryCodeHandler(
        SignInManager<IdentityUser> signInManager,
        IResultService resultService,
         ILogger<LoginHandler> logger
        ) : IHandler<LoginWithRecoveryCodeRequest , Response>
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LoginHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(LoginWithRecoveryCodeRequest request)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            var recoveryCode = request.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return _resultService.Success<Response>(new RegisterResponse
                {
                    Message = $"User with ID '{user.Id}' logged in with a recovery code.",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return _resultService.Error<Response>($"User with ID '{user.Id}' account locked out.");
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                return _resultService.Error<Response>("Invalid recovery code entered.");
            }
        }
    }
}

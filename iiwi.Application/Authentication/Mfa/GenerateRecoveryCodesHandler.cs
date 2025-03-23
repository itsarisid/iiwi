using Architecture.Application.Authentication.Mfa;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class GenerateRecoveryCodesHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<GenerateRecoveryCodesHandler> logger,
    IResultService resultService) : IHandler<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<GenerateRecoveryCodesHandler> _logger = logger;

        public async Task<Result<GenerateRecoveryCodesResponse>> HandleAsync(GenerateRecoveryCodesRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<GenerateRecoveryCodesResponse>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            var RecoveryCodes = recoveryCodes?.ToArray();

            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);

            //Note: Show Recovery Codes
            return _resultService.Success(new GenerateRecoveryCodesResponse
            {
                RecoveryCodes= RecoveryCodes,
                Message = "You have generated new recovery codes.",
            });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class EnableAuthenticatorHandler(
    UserManager<IdentityUser> userManager,
    UrlEncoder urlEncoder,
    IClaimsProvider claimsProvider,
    ILogger<EnableAuthenticatorHandler> logger,
    IResultService resultService) : IHandler<EnableAuthenticatorRequest, EnableAuthenticatorResponse>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly UrlEncoder _urlEncoder = urlEncoder;
        private readonly ILogger<EnableAuthenticatorHandler> _logger = logger;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public async Task<Result<EnableAuthenticatorResponse>> HandleAsync(EnableAuthenticatorRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<EnableAuthenticatorResponse>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            // Strip spaces and hypens
            var verificationCode = request.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                return _resultService.Error<EnableAuthenticatorResponse>("Verification code is invalid.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

            //StatusMessage = "Your authenticator app has been verified.";

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var email = await _userManager.GetEmailAsync(user);

            if (await _userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                //ShowRecoveryCodes";
                return _resultService.Success(new EnableAuthenticatorResponse
                {
                    RecoveryCodes = recoveryCodes?.ToArray(),
                    AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey),
                    Message = "Your authenticator app has been verified."
                });
            }
            else
            {
                return _resultService.Error<EnableAuthenticatorResponse>("Verification code is invalid.");
            }
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode("IdentityStandaloneMfa"),
                _urlEncoder.Encode(email ?? ""),
                unformattedKey);
        }
    }
}


using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Encodings.Web;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication
{
    /// <summary>
    /// Handler for enabling two-factor authentication.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_urlEncoder">The URL encoder.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class EnableAuthenticatorHandler(
    UserManager<ApplicationUser> _userManager,
    UrlEncoder _urlEncoder,
    IClaimsProvider _claimsProvider,
    ILogger<EnableAuthenticatorHandler> _logger) : IHandler<EnableAuthenticatorRequest, EnableAuthenticatorResponse>
    {

        /// <summary>
        /// Handles the enable authenticator request asynchronously.
        /// </summary>
        /// <param name="request">The enable authenticator request.</param>
        /// <summary>
        /// Enables two-factor authentication for the current user using the provided authenticator verification code.
        /// </summary>
        /// <param name="request">The request containing the authenticator verification code.</param>
        /// <returns>
        /// A result wrapping an <see cref="EnableAuthenticatorResponse"/>:
        /// - On success (HTTP 200): contains generated recovery codes, an authenticator QR code URI, and a success message.
        /// - On failure (HTTP 400): contains an error message describing why the enable operation did not complete.
        /// </returns>
        public async Task<Result<EnableAuthenticatorResponse>> HandleAsync(EnableAuthenticatorRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<EnableAuthenticatorResponse>(HttpStatusCode.BadRequest, new EnableAuthenticatorResponse
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var verificationCode = request.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                return new Result<EnableAuthenticatorResponse>(HttpStatusCode.BadRequest, new EnableAuthenticatorResponse
                {
                    Message = "Verification code is invalid."
                });
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);


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
                return new Result<EnableAuthenticatorResponse>(HttpStatusCode.OK, new EnableAuthenticatorResponse
                {
                    RecoveryCodes = recoveryCodes?.ToArray(),
                    AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey),
                    Message = "Your authenticator app has been verified."
                });
            }
            else
            {
                return new Result<EnableAuthenticatorResponse>(HttpStatusCode.BadRequest, new EnableAuthenticatorResponse
                {
                    Message = "Verification code is invalid."
                });
            }
        }


        /// <summary>
        /// Generates the QR code URI.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <param name="unformattedKey">The unformatted key.</param>
        /// <summary>
        /// Builds the authenticator provisioning URI used to generate a QR code for an authenticator app.
        /// </summary>
        /// <param name="email">The user's email address to include in the URI; may be null or empty.</param>
        /// <param name="unformattedKey">The authenticator key in unformatted form to include in the URI.</param>
        /// <returns>The formatted authenticator provisioning URI with the path and email URL-encoded and the provided key included.</returns>
        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
            General.AuthenticatorUriFormat,
                _urlEncoder.Encode("IdentityStandaloneMfa"),
                _urlEncoder.Encode(email ?? ""),
                unformattedKey);
        }
    }
}

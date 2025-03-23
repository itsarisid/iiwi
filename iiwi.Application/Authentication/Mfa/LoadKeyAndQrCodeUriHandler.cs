using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Text.Encodings.Web;

namespace Architecture.Application.Authentication
{
    internal class LoadKeyAndQrCodeUriHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    UrlEncoder urlEncoder,
    IResultService resultService) : IHandler<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly UrlEncoder _urlEncoder = urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public async Task<Result<LoadKeyAndQrCodeUriResponse>> HandleAsync(LoadKeyAndQrCodeUriRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<LoadKeyAndQrCodeUriResponse>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var email = await _userManager.GetEmailAsync(user);

           return _resultService.Success(new LoadKeyAndQrCodeUriResponse
            {
                SharedKey = FormatKey(unformattedKey),
                AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey)
            });
        }

        private static string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey?.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey?.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
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

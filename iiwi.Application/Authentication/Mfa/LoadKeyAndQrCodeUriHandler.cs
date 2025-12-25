using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for loading the authenticator key and QR code URI.
/// </summary>
/// <param name="_userManager">The user manager.</param>
/// <param name="_claimsProvider">The claims provider.</param>
/// <param name="_urlEncoder">The URL encoder.</param>
internal class LoadKeyAndQrCodeUriHandler(
UserManager<ApplicationUser> _userManager,
IClaimsProvider _claimsProvider,
UrlEncoder _urlEncoder) : IHandler<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>
{

    /// <summary>
    /// Handles the load key and QR code URI request asynchronously.
    /// </summary>
    /// <param name="request">The load key and QR code URI request.</param>
    /// <summary>
    /// Loads the current user's authenticator key and the corresponding QR code URI for TOTP setup.
    /// </summary>
    /// <param name="request">The request triggering the load operation.</param>
    /// <returns>`Result` containing a `LoadKeyAndQrCodeUriResponse` with `SharedKey` (formatted authenticator key) and `AuthenticatorUri` on success; returns a `BadRequest` result with an error message if the current user cannot be loaded.</returns>
    public async Task<Result<LoadKeyAndQrCodeUriResponse>> HandleAsync(LoadKeyAndQrCodeUriRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<LoadKeyAndQrCodeUriResponse>(HttpStatusCode.BadRequest, new LoadKeyAndQrCodeUriResponse
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(unformattedKey))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var email = await _userManager.GetEmailAsync(user);

        return new Result<LoadKeyAndQrCodeUriResponse>(HttpStatusCode.OK, new LoadKeyAndQrCodeUriResponse
        {
            SharedKey = FormatKey(unformattedKey),
            AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey)
        });
    }


    /// <summary>
    /// Formats the authenticator key.
    /// </summary>
    /// <param name="unformattedKey">The unformatted key.</param>
    /// <summary>
    /// Formats an authenticator key by inserting a space every four characters and converting the result to lowercase.
    /// </summary>
    /// <param name="unformattedKey">The authenticator key to format; may be null or empty.</param>
    /// <returns>The formatted key with groups of up to four characters separated by spaces and converted to lowercase; returns an empty string if <paramref name="unformattedKey"/> is null or empty.</returns>
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


    /// <summary>
    /// Generates the QR code URI.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="unformattedKey">The unformatted key.</param>
    /// <summary>
    /// Builds the authenticator QR code URI for the given account email and authenticator key.
    /// </summary>
    /// <param name="email">Account email used as the authenticator label; empty string if null.</param>
    /// <param name="unformattedKey">The raw authenticator key to embed in the URI.</param>
    /// <returns>The formatted authenticator URI suitable for generating a QR code.</returns>
    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
        General.AuthenticatorUriFormat,
            _urlEncoder.Encode("IdentityStandaloneMfa"),
            _urlEncoder.Encode(email ?? ""),
            unformattedKey);
    }
}
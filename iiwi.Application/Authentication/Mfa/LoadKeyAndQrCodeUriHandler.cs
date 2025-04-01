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

internal class LoadKeyAndQrCodeUriHandler(
UserManager<ApplicationUser> _userManager,
IClaimsProvider _claimsProvider,
UrlEncoder _urlEncoder) : IHandler<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.LoadKeyAndQrCodeUriRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Authentication.LoadKeyAndQrCodeUriResponse>>.</returns>
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
    ///  Function Name :  FormatKey.
    /// </summary>
    /// <param name="unformattedKey">This unformattedKey's Datatype is : string.</param>
    /// <returns>string.</returns>
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
    ///  Function Name :  GenerateQrCodeUri.
    /// </summary>
    /// <param name="email">This email's Datatype is : string.</param>
    /// <param name="unformattedKey">This unformattedKey's Datatype is : string.</param>
    /// <returns>string.</returns>
    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
        General.AuthenticatorUriFormat,
            _urlEncoder.Encode("IdentityStandaloneMfa"),
            _urlEncoder.Encode(email ?? ""),
            unformattedKey);
    }
}

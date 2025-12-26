using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for generating recovery codes.
/// </summary>
/// <param name="_userManager">The user manager.</param>
/// <param name="_claimsProvider">The claims provider.</param>
/// <param name="_logger">The logger.</param>
public class GenerateRecoveryCodesHandler(
UserManager<ApplicationUser> _userManager,
IClaimsProvider _claimsProvider,
ILogger<GenerateRecoveryCodesHandler> _logger) : IHandler<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>
{

    /// <summary>
    /// Handles the generate recovery codes request asynchronously.
    /// </summary>
    /// <param name="request">The generate recovery codes request.</param>
    /// <summary>
    /// Generates new two-factor authentication recovery codes for the current user.
    /// </summary>
    /// <param name="request">The request object for generation; not used by this handler but part of the handler contract.</param>
    /// <returns>
    /// A Result containing a GenerateRecoveryCodesResponse:
    /// - On success (HTTP 200): Response.RecoveryCodes contains the new codes and Message confirms generation.
    /// - On failure (HTTP 400): Response.Message contains an error indicating the user could not be loaded.
    /// </returns>
    /// <summary>
    /// Generates new two-factor authentication recovery codes for the current user.
    /// </summary>
    /// <param name="request">Marker request required by the handler contract; this handler does not use any data from the request.</param>
    /// <returns>A Result containing a GenerateRecoveryCodesResponse. On success (HTTP 200) the response includes the generated recovery codes and a success message. If the current user cannot be loaded the result is HTTP 400 with an explanatory message.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the current user does not have two-factor authentication enabled.</exception>
    public async Task<Result<GenerateRecoveryCodesResponse>> HandleAsync(GenerateRecoveryCodesRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<GenerateRecoveryCodesResponse>(HttpStatusCode.BadRequest, new GenerateRecoveryCodesResponse
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
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

        return new Result<GenerateRecoveryCodesResponse>(HttpStatusCode.OK, new GenerateRecoveryCodesResponse
        {
            RecoveryCodes = RecoveryCodes,
            Message = "You have generated new recovery codes.",
        });
    }
}
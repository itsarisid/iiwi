using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication
{
    public class GenerateRecoveryCodesHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    ILogger<GenerateRecoveryCodesHandler> _logger) : IHandler<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>
    {

        /// <summary>
        ///  Function Name :  HandleAsync.
        /// </summary>
        /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.GenerateRecoveryCodesRequest.</param>
        /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Authentication.GenerateRecoveryCodesResponse>>.</returns>
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
}

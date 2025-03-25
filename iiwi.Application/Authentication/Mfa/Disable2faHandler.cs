using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class Disable2faHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    ILogger<Disable2faHandler> _logger) : IHandler<Disable2faRequest, Response>
    {
        public async Task<Result<Response>> HandleAsync(Disable2faRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(_claimsProvider.ClaimsPrinciple));
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "2fa has been disabled. You can re enable 2fa when you setup an authenticator app and call TwoFactorAuthentication",
            });
        }
    }
}

using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class AccountStatusHandler(
UserManager<ApplicationUser> userManager,
SignInManager<ApplicationUser> signInManager,
IClaimsProvider claimsProvider) : IHandler<AccountStatusRequest, AccountStatusResponse>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;

    public async Task<Result<AccountStatusResponse>> HandleAsync(AccountStatusRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<AccountStatusResponse>(HttpStatusCode.BadRequest, new AccountStatusResponse
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        return new Result<AccountStatusResponse>(HttpStatusCode.OK, new AccountStatusResponse
        {
            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user),
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user)
        });
    }
}


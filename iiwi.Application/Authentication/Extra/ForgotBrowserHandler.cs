using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication.Extra;

public class ForgotBrowserHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider) : IHandler<ForgotBrowserRequest, Response>
{
    public async Task<Result<Response>> HandleAsync(ForgotBrowserRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        await _signInManager.ForgetTwoFactorClientAsync();
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code."
        });
    }
}

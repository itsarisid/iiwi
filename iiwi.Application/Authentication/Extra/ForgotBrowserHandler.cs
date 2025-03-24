using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class ForgotBrowserHandler(
UserManager<IdentityUser> userManager,
SignInManager<IdentityUser> signInManager,
IClaimsProvider claimsProvider) : IHandler<ForgotBrowserRequest, Response>
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;

    public async Task<Result<Response>> HandleAsync(ForgotBrowserRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        await _signInManager.ForgetTwoFactorClientAsync();
        return new Result<Response>(HttpStatusCode.OK,new Response
        {
            Message = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code."
        });
    }
}


using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class RemoveLoginHandler(
UserManager<ApplicationUser> userManager,
SignInManager<ApplicationUser> signInManager,
IClaimsProvider claimsProvider) : IHandler<RemoveLoginRequest, Response>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;

    public async Task<Result<Response>> HandleAsync(RemoveLoginRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var result = await _userManager.RemoveLoginAsync(user, request.LoginProvider, request.ProviderKey);
        if (!result.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = "The external login was not removed."
            });
        }

        await _signInManager.RefreshSignInAsync(user);
        return new Result<Response>(HttpStatusCode.BadRequest, new Response
        {
            Message = "The external login was removed.",
        });
    }
}


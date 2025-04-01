using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

public class RemoveLoginHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager,
IClaimsProvider _claimsProvider) : IHandler<RemoveLoginRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.RemoveLoginRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
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


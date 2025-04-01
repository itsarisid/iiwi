using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

public class ConfirmEmailChangeHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager) : IHandler<ConfirmEmailChangeRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.ConfirmEmailChangeRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(ConfirmEmailChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{request.UserId}'."
            });
        }

        request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
        var result = await _userManager.ChangeEmailAsync(user, request.Email, request.Code);
        if (!result.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.InternalServerError, new Response
            {
                Message = "Error changing email."
            });
        }

        var setUserNameResult = await _userManager.SetUserNameAsync(user, request.Email);
        if (!setUserNameResult.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.InternalServerError, new Response
            {
                Message = "Error changing user name."
            });
        }

        await _signInManager.RefreshSignInAsync(user);

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Thank you for confirming your email change."
        });
    }
}

using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace iiwi.Application.Authentication.Email;

public class ConfirmEmailChangeHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager) : IHandler<ConfirmEmailChangeRequest, Response>
{
    public async Task<Result<Response>> HandleAsync(ConfirmEmailChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
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

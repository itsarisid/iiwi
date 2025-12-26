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
    /// <summary>
    /// Confirms and applies a user's email change using the provided confirmation code.
    /// </summary>
    /// <param name="request">The request containing the user's ID, the new email, and the encoded confirmation code.</param>
    /// <returns>
    /// A Result wrapping a Response with a message and an HTTP status code indicating the outcome:
    /// 200 OK on success, 404 NotFound if the user cannot be found, or 500 InternalServerError if changing the email or username fails.
    /// <summary>
    /// Confirms a user's email change using the provided request, updates the user's username to the new email, and refreshes the sign-in session.
    /// </summary>
    /// <param name="request">Request containing the target user's ID, the new email, and the Base64 URL-encoded confirmation code.</param>
    /// <returns>A Result&lt;Response&gt; with HTTP status and a message: 200 OK with a confirmation message on success; 404 NotFound if the user cannot be loaded; 500 InternalServerError if changing the email or username fails.</returns>
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
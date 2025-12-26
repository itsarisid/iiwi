using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace iiwi.Application.Authentication.Email;

public class ConfirmEmailHandler(UserManager<ApplicationUser> _userManager) : IHandler<ConfirmEmailRequest, Response>
{
    /// <summary>
    /// Confirms a user's email using the confirmation code supplied in the request.
    /// </summary>
    /// <param name="request">Request containing the target user's Id and the email confirmation code.</param>
    /// <returns>
    /// A Result&lt;Response&gt; containing an HTTP status and message:
    /// `200 OK` with a success message when the email is confirmed,
    /// `404 NotFound` with an error message when the user cannot be found,
    /// `500 InternalServerError` with an error message when confirmation fails.
    /// <summary>
    /// Confirms a user's email using the provided user ID and URL-safe Base64 encoded confirmation code.
    /// </summary>
    /// <param name="request">Contains the target user's ID and the URL-safe Base64 encoded confirmation code (request.Code).</param>
    /// <returns>
    /// A Result wrapping a Response:
    /// - HTTP 200 OK with a success message when the email is confirmed.
    /// - HTTP 404 NotFound with an error message when the user cannot be found.
    /// - HTTP 500 InternalServerError with an error message when email confirmation fails.
    /// </returns>
    public async Task<Result<Response>> HandleAsync(ConfirmEmailRequest request)
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
        var result = await _userManager.ConfirmEmailAsync(user, request.Code);
        if (result.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Thank you for confirming your email."
            });
        }
        else
        {
            return new Result<Response>(HttpStatusCode.InternalServerError, new Response
            {
                Message = "Error confirming your email."
            });
        }
    }
}
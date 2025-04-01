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

public class ResetPasswordHandler(
UserManager<ApplicationUser> _userManager) : IHandler<ResetPasswordRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.ResetPasswordRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(ResetPasswordRequest request)
    {
        request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with email '{request.Email}'."
            });
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);
        if (result.Succeeded)
        {
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Your password has been reset"
            });
        }

        return new Result<Response>(HttpStatusCode.InternalServerError, new Response
        {
            Message = "Error..."
        });

    }
}

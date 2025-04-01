using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication
{
    public class LinkLoginHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<LinkLoginHandler> _logger) : IHandler<LinkLoginRequest, Response>
    {

        /// <summary>
        ///  Function Name :  HandleAsync.
        /// </summary>
        /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.LinkLoginRequest.</param>
        /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
        public async Task<Result<Response>> HandleAsync(LinkLoginRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }





            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Method not implemented",
            });
        }
    }
}

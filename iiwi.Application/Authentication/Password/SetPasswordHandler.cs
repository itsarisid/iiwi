using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication;

public class SetPasswordHandler(
UserManager<ApplicationUser> userManager,
SignInManager<ApplicationUser> signInManager,
IClaimsProvider claimsProvider,
ILogger<SetPasswordHandler> logger) : IHandler<SetPasswordRequest, Response>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;
    private readonly ILogger<SetPasswordHandler> _logger = logger;

    public async Task<Result<Response>> HandleAsync(SetPasswordRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result <Response>(HttpStatusCode.BadRequest,new Response
            {
               Message= $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);

        if (hasPassword)
        {
            //Note: goto change password
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = "Password already set"
            });
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            _logger.LogError("Error while setting password");
            return new Result<Response>(HttpStatusCode.InternalServerError, new Response
            {
                Message = "Error while setting password"
            });
        }

        await _signInManager.RefreshSignInAsync(user);
        return new Result<Response>(HttpStatusCode.OK,new Response
        {
            Message = "Your password has been set.",
        });
    }
}

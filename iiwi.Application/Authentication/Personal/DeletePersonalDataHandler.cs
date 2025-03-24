using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication;
public class DeletePersonalDataHandler(
UserManager<IdentityUser> userManager,
SignInManager<IdentityUser> signInManager,
IClaimsProvider claimsProvider,
ILogger<LoginHandler> logger) : IHandler<DeletePersonalDataRequest, Response>
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;
    private readonly ILogger<LoginHandler> _logger = logger;

    public async Task<Result<Response>> HandleAsync(DeletePersonalDataRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var RequirePassword = await _userManager.HasPasswordAsync(user);
        if (RequirePassword)
        {
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = "Incorrect password."
                });
            }
        }

        var result = await _userManager.DeleteAsync(user);
        var userId = await _userManager.GetUserIdAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
        }

        await _signInManager.SignOutAsync();

        _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Your record has been deleted",
        });
    }
}


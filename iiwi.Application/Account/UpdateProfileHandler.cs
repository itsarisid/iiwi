using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Authentication;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Account;

public class UpdateProfileHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    ILogger<UpdateProfileHandler> _logger) : IHandler<UpdateProfileRequest, Response>
{
    public async Task<Result<Response>> HandleAsync(UpdateProfileRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        user.Address = request.Address;
        user.DOB = request.DOB;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DisplayName = request.DisplayName;
        user.Gender = request.Gender;

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("User updated their profile successfully.");

        //StatusMessage = "The external login was added.";
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "The external login was added.",
        });
    }
}

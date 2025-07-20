using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Account.
/// </summary>
namespace iiwi.Application.Account;

public class UpdateProfileHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    IPermissionRepository permission,
    ILogger<UpdateProfileHandler> _logger) : IHandler<UpdateProfileRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Account.UpdateProfileRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
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

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Profile updated successfully.",
        });
    }
}

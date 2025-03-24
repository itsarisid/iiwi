using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class UpdatePhoneNumberHandler(
UserManager<IdentityUser> userManager,
SignInManager<IdentityUser> signInManager,
IClaimsProvider claimsProvider) : IHandler<UpdatePhoneNumberRequest, Response>
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;

    public async Task<Result<Response>> HandleAsync(UpdatePhoneNumberRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
            }
        }

        await _signInManager.RefreshSignInAsync(user);
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Your profile has been updated",
        });
    }
}

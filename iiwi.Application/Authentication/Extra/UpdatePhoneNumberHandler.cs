using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication.Extra;

public class UpdatePhoneNumberHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider) : IHandler<UpdatePhoneNumberRequest, Response>
{
    /// <summary>
    /// Updates the current user's phone number if it differs from the provided value and refreshes the user's sign-in state.
    /// </summary>
    /// <param name="request">The request containing the new phone number to set for the current user.</param>
    /// <returns>A Result containing a Response: `200 OK` with a success message when the update completes, or `404 NotFound` if the current user cannot be loaded.</returns>
    /// <summary>
    /// Updates the current user's phone number if it differs from the provided value and refreshes the user's sign-in state.
    /// </summary>
    /// <param name="request">The update request containing the desired phone number.</param>
    /// <returns>
    /// A Result containing a Response:
    /// - 200 OK with a success message when the phone number is updated (or unchanged) and sign-in is refreshed.
    /// - 404 NotFound with an error message if the current user cannot be loaded from the claims principal.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown if saving the new phone number fails unexpectedly.</exception>
    public async Task<Result<Response>> HandleAsync(UpdatePhoneNumberRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
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
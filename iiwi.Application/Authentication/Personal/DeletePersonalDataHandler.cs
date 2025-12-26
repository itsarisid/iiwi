using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication.Personal;

public class DeletePersonalDataHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<DeletePersonalDataHandler> _logger) : IHandler<DeletePersonalDataRequest, Response>
{
    /// <summary>
    /// Deletes the currently authenticated user's account, optionally validating the supplied password before deletion.
    /// </summary>
    /// <param name="request">The request containing the password to validate when the account requires one.</param>
    /// <returns>
    /// A Result&lt;Response&gt; containing an HTTP status and message:
    /// - 404 NotFound if the current user cannot be loaded.
    /// - 400 BadRequest if a required password is provided and is incorrect.
    /// - 200 OK when the account was successfully deleted.
    /// </returns>
    /// <summary>
    /// Deletes the currently authenticated user's account, validating the supplied password when the account requires one.
    /// </summary>
    /// <param name="request">Request data, including the current password when required for validation.</param>
    /// <returns>A Result wrapping a Response and an HTTP status code: NotFound if the user cannot be loaded, BadRequest if the provided password is incorrect, and OK when the account is deleted.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the user deletion operation fails.</exception>
    public async Task<Result<Response>> HandleAsync(DeletePersonalDataRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
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
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class LogoutHandler(
    SignInManager<ApplicationUser> signInManager
    ) : IHandler<LogoutRequest, Response>
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<Result<Response>> HandleAsync(LogoutRequest request)
    {
        await _signInManager.SignOutAsync();

        return new Result<Response>(HttpStatusCode.OK, new Response {
            Message="Logout successfully"
        });
    }
}

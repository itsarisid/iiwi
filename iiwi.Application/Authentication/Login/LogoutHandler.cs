using DotNetCore.Mediator;
using DotNetCore.Results;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class LogoutHandler(
    SignInManager<IdentityUser> signInManager
    ) : IHandler<LogoutRequest, Response>
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<Result<Response>> HandleAsync(LogoutRequest request)
    {
        await _signInManager.SignOutAsync();

        return new Result<Response>(HttpStatusCode.OK, new Response {
            Message="Logout successfully"
        });
    }
}

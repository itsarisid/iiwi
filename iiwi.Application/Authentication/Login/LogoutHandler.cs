using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class LogoutHandler(
        SignInManager<IdentityUser> signInManager,
        IResultService resultService
        ) : IHandler<LogoutRequest, Response>
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(LogoutRequest request)
        {
            await _signInManager.SignOutAsync();

            return _resultService.Success(new Response {
                Message="Logout successfully"
            });
        }
    }
}

using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication
{
    public class LoginHandler(
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ILogger<ApplicationUser> _logger
        ) : IHandler<LoginRequest, Response>
    {
        public async Task<Result<Response>> HandleAsync(LoginRequest request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                //return new Result<Response>(HttpStatusCode.OK, new Response
                //{
                //    Message = "User logged in."
                //});
                return GenerateTokenAsync(request);
            }
            if (result.RequiresTwoFactor)
            {
                _logger.LogInformation("Login with 2fa");
                return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "Login with 2fa",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "User account locked out.",
                });
            }
            else
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new RegisterResponse
                {
                    Message = "Invalid login attempt."
                });
            }
        }


        private async Task<Result<RegisterResponse>> GenerateTokenAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Result<RegisterResponse>(HttpStatusCode.BadRequest, new RegisterResponse
                {
                    Message = "Invalid login attempt."
                });
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ACDt1vR3lXToPQ1g3MyN"));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = ,
            //    Expires = DateTime.UtcNow.AddMinutes(5),
            //    Issuer = "Harmony",
            //    Audience = "Harmony",
            //    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature)
            //};

            //var token = GetToken(authClaims);
            //var token = _jwtService.Encode(claims);

            //return new Result<Response>(HttpStatusCode.BadRequest, new RegisterResponse
            //{
            //    Message = "Invalid login attempt.",
            //    FullName = "Sajid Khan",
            //    Token = token
            //});

            return new Result<RegisterResponse>(HttpStatusCode.OK, new RegisterResponse
            {
                Message = "JWT Token",
                FullName = "Sajid Khan",
                Token = GetToken(authClaims)
            });
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "iiwi",
                audience: "iiwi",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

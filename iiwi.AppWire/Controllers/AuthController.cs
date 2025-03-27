using DotNetCore.AspNetCore;
using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.AppWire.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseController
{
    /// <summary>Registers this instance.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterRequest request) => Mediator.HandleAsync<RegisterRequest, RegisterResponse>(request).ApiResult();

    /// <summary>
    /// Authentications the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Auth
    ///     {
    ///        "login": "sajid",
    ///        "password": "khan"
    ///     }
    ///
    /// </remarks>
    /// <returns>Authorized user</returns>
    /// <response code="201">Authorized user</response>
    /// <response code="400">If the user is null</response>
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequest request) => Mediator.HandleAsync<LoginRequest, Response>(request).ApiResult();
    
    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login(string ReturnUrl) => Ok(ReturnUrl);

    [HttpGet("denied")]
    [AllowAnonymous]
    public IActionResult Denied() => Unauthorized("Unauthorized");

    /// <summary>Log out this instance.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("logout")]
    [AllowAnonymous]
    public IActionResult Logout() => Mediator.HandleAsync<LogoutRequest, Response>(new LogoutRequest()).ApiResult();

    /// <summary>Forgot the password.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public IActionResult ForgotPassword(ForgotPasswordRequest request) => Mediator.HandleAsync<ForgotPasswordRequest, Response>(request).ApiResult();

    /// <summary>Resets the password.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public IActionResult ResetPassword(ResetPasswordRequest request) => Mediator.HandleAsync<ResetPasswordRequest, Response>(request).ApiResult();

    /// <summary>Registers the confirmation.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("register-confirmation")]
    [AllowAnonymous]
    public IActionResult RegisterConfirmation(RegisterConfirmationRequest request) => Mediator.HandleAsync<RegisterConfirmationRequest, Response>(request).ApiResult();

    /// <summary>Confirms the email change.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpGet("confirm-email-change")]
    [AllowAnonymous]
    public IActionResult ConfirmEmailChange(ConfirmEmailChangeRequest request) => Mediator.HandleAsync<ConfirmEmailChangeRequest, Response>(request).ApiResult();

    /// <summary>Confirms the email.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public IActionResult ConfirmEmail(ConfirmEmailRequest request) => Mediator.HandleAsync<ConfirmEmailRequest, Response>(request).ApiResult();

    /// <summary>Your login is protected with an authenticator app. Enter your authenticator code below.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.InvalidOperationException">Unable to load two-factor authentication user.</exception>
    [HttpPost("login-with-2fa")]
    [AllowAnonymous]
    public IActionResult LoginWith2fa(LoginWith2faRequest request) => Mediator.HandleAsync<LoginWith2faRequest, Response>(request).ApiResult();

    /// <summary>You have requested to log in with a recovery code.
    /// an authenticator app code at log in or disable 2FA and log in again.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("login-with-recovery-code")]
    [AllowAnonymous]
    public IActionResult LoginWithRecoveryCode(LoginWithRecoveryCodeRequest request) => Mediator.HandleAsync<LoginWithRecoveryCodeRequest, Response>(request).ApiResult();
}

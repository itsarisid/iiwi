using DotNetCore.AspNetCore;
using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.AppWire.Controllers;

/// <summary>Multi-factor authentication (MFA) adds a layer of protection to the sign-in process.</summary>
[Route("api/v{version:apiVersion}/auth/[controller]")]
public class MFAController : BaseController
{
    /// <summary>Deletes the personal data.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("load-key")]
    public IActionResult LoadKeyAndQrCodeUri() => Mediator.HandleAsync<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>(new LoadKeyAndQrCodeUriRequest()).ApiResult();

    /// <summary>Enables the authenticator.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("enable-authenticator")]
    public IActionResult EnableAuthenticator(EnableAuthenticatorRequest request) => Mediator.HandleAsync<EnableAuthenticatorRequest, EnableAuthenticatorResponse>(request).ApiResult();

    /// <summary>Gets the external logins.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("external-logins")]
    public IActionResult GetExternalLogins() => Mediator.HandleAsync<ExternalLoginsRequest, ExternalLoginsResponse>(new ExternalLoginsRequest()).ApiResult();

    /// <summary>Posts the remove login.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("remove-login")]
    [AllowAnonymous]
    public IActionResult RemoveLogin(RemoveLoginRequest request) => Mediator.HandleAsync<RemoveLoginRequest, Response>(request).ApiResult();

    /// <summary>Posts the link login.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("link-login")]
    public IActionResult LinkLogin(LinkLoginRequest request) => Mediator.HandleAsync<LinkLoginRequest, Response>(request).ApiResult();

    /// <summary>Gets the link login callback.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.InvalidOperationException">Unexpected error occurred loading external login info for user with ID '{user.Id}'.</exception>
    [HttpPost("link-login-callback")]
    public IActionResult LinkLoginCallback() => Mediator.HandleAsync<LinkLoginCallbackRequest, Response>(new LinkLoginCallbackRequest()).ApiResult();

    /// <summary>Put these codes in a safe place. If you lose your device and don't have the recovery codes you will lose access to your account.
    ///  Generating new recovery codes does not change the keys used in authenticator apps.
    ///  If you wish to change the key used in an authenticator app you should reset your authenticator keys.
    /// </summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("generate-recovery-codes")]
    public IActionResult GenerateRecoveryCodes() => Mediator.HandleAsync<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>(new GenerateRecoveryCodesRequest()).ApiResult();

    /// <summary>Updates the phone number.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.InvalidOperationException">Unexpected error occurred setting phone number for user with ID '{userId}'.</exception>
    [HttpPost("update-phone-number")]
    public IActionResult UpdatePhoneNumber(UpdatePhoneNumberRequest request) => Mediator.HandleAsync<UpdatePhoneNumberRequest, Response>(request).ApiResult();

    /// <summary>If you reset your authenticator key your authenticator app will not work until you reconfigure it.
    ///This process disables 2FA until you verify your authenticator app.If you do not complete your authenticator app configuration you may lose access to your account.
    /// </summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("reset-authenticator")]
    public IActionResult ResetAuthenticator() => Mediator.HandleAsync<ResetAuthenticatorRequest, Response>(new ResetAuthenticatorRequest()).ApiResult();

    /// <summary>You do not have a local user name/password for this site. Add a local
    /// account so you can log in without an external login.</summary>
    /// <param name="request"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("set-password")]
    public IActionResult SetPassword(SetPasswordRequest request) => Mediator.HandleAsync<SetPasswordRequest, Response>(request).ApiResult();

    /// <summary>Accounts the status.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("account-status")]
    public IActionResult AccountStatus() => Mediator.HandleAsync<AccountStatusRequest, AccountStatusResponse>(new AccountStatusRequest()).ApiResult();

    /// <summary>Forgot the browser.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("forgot-browser")]
    public IActionResult ForgotBrowser() => Mediator.HandleAsync<ForgotBrowserRequest, Response>(new ForgotBrowserRequest()).ApiResult();

    /// <summary>Disabling 2FA does not change the keys used in authenticator apps. If you wish to change the key
    /// used in an authenticator app you should reset your authenticator keys..</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpPost("disable-2fa")]
    public IActionResult Disable2fa() => Mediator.HandleAsync<Disable2faRequest, Response>(new Disable2faRequest()).ApiResult();
}

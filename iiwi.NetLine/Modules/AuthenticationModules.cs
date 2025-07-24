using iiwi.Application;
using iiwi.Application.Authentication;
using iiwi.NetLine.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

/// <summary>
/// Provides endpoints for user authentication and account security management
/// </summary>
/// <remarks>
/// This module handles all authentication-related operations including:
/// - Two-factor authentication setup and management
/// - External login provider integration
/// - Password management
/// - Account recovery options
/// All endpoints require authorization except where noted.
/// </remarks>
public class AuthenticationModules : IEndpoints
{
    /// <summary>
    /// Registers all authentication-related endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Create an authorized route group for all endpoints
        var routeGroup = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Authentication.Group)
            .RequireAuthorization();

        /// <summary>
        /// [GET] /authentication/load-key-qrcode - Generates 2FA setup data
        /// </summary>
        /// <remarks>
        /// Generates a new shared secret key and QR code URI for setting up 
        /// an authenticator app. The QR code can be scanned by apps like 
        /// Google Authenticator or Microsoft Authenticator.
        /// 
        /// Returns:
        /// - Shared key (for manual entry)
        /// - QR code URI (for scanning)
        /// - Formatted authenticator URI
        /// </remarks>
        /// <returns>2FA setup data</returns>
        /// <response code="200">Returns 2FA setup data</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapGet(Authentication.LoadKeyAndQrCodeUri.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>(new LoadKeyAndQrCodeUriRequest())
            .Response())
            .WithDocumentation(Authentication.LoadKeyAndQrCodeUri);

        /// <summary>
        /// [POST] /authentication/enable-authenticator - Activates 2FA
        /// </summary>
        /// <remarks>
        /// Verifies the provided verification code and enables two-factor
        /// authentication for the user account. The code should be generated
        /// by the authenticator app using the shared key from the setup endpoint.
        /// </remarks>
        /// <param name="request">Contains verification code and shared key</param>
        /// <returns>2FA activation result</returns>
        /// <response code="200">2FA enabled successfully</response>
        /// <response code="400">Invalid verification code</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapPost(Authentication.EnableAuthenticator.Endpoint,
            IResult (IMediator mediator, EnableAuthenticatorRequest request) => mediator
            .HandleAsync<EnableAuthenticatorRequest, EnableAuthenticatorResponse>(request)
            .Response())
            .WithDocumentation(Authentication.EnableAuthenticator);

        /// <summary>
        /// [GET] /authentication/external-logins - Lists external logins
        /// </summary>
        /// <remarks>
        /// Retrieves all external authentication providers (Google, Facebook, etc.)
        /// currently linked to the user's account.
        /// </remarks>
        /// <returns>List of external login providers</returns>
        /// <response code="200">Returns linked external logins</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapGet(Authentication.ExternalLogins.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ExternalLoginsRequest, ExternalLoginsResponse>(new ExternalLoginsRequest())
            .Response())
            .WithDocumentation(Authentication.ExternalLogins);

        /// <summary>
        /// [DELETE] /authentication/remove-login - Unlinks external login
        /// </summary>
        /// <remarks>
        /// Removes the specified external login provider association from the
        /// user's account. The user will no longer be able to sign in using
        /// this provider.
        /// </remarks>
        /// <param name="request">Contains provider info to remove</param>
        /// <returns>Operation result</returns>
        /// <response code="204">Login provider removed</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Provider not found</response>
        routeGroup.MapDelete(Authentication.RemoveLogin.Endpoint,
            IResult (IMediator mediator, [FromBody] RemoveLoginRequest request) => mediator
            .HandleAsync<RemoveLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.RemoveLogin);

        /// <summary>
        /// [POST] /authentication/link-login - Links new external login
        /// </summary>
        /// <remarks>
        /// Initiates the process to link a new external authentication provider
        /// to the user's account. This typically redirects to the provider's
        /// authentication page.
        /// </remarks>
        /// <param name="request">Provider to link</param>
        /// <returns>Challenge result</returns>
        /// <response code="302">Redirect to provider</response>
        /// <response code="400">Invalid provider</response>
        routeGroup.MapPost(Authentication.LinkLogin.Endpoint,
            IResult (IMediator mediator, LinkLoginRequest request) => mediator
            .HandleAsync<LinkLoginRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.LinkLogin);

        /// <summary>
        /// [GET] /authentication/link-login-callback - External login callback
        /// </summary>
        /// <remarks>
        /// Callback endpoint that completes the external login linking process
        /// after successful authentication with the provider.
        /// </remarks>
        /// <returns>Link operation result</returns>
        /// <response code="200">Login linked successfully</response>
        /// <response code="400">Linking failed</response>
        routeGroup.MapGet(Authentication.LinkLoginCallback.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<LinkLoginCallbackRequest, Response>(new LinkLoginCallbackRequest())
            .Response())
            .WithDocumentation(Authentication.LinkLoginCallback);

        /// <summary>
        /// [POST] /authentication/generate-recovery-codes - Creates recovery codes
        /// </summary>
        /// <remarks>
        /// Generates new recovery codes that can be used to access the account
        /// when two-factor authentication is unavailable. These should be stored
        /// securely as they will only be displayed once.
        /// 
        /// Note: Generating new codes invalidates any existing recovery codes.
        /// </remarks>
        /// <returns>New recovery codes</returns>
        /// <response code="200">Returns new recovery codes</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapPost(Authentication.GenerateRecoveryCodes.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>(new GenerateRecoveryCodesRequest())
            .Response())
            .WithDocumentation(Authentication.GenerateRecoveryCodes);

        /// <summary>
        /// [POST] /authentication/reset-authenticator - Resets 2FA setup
        /// </summary>
        /// <remarks>
        /// Resets the authenticator key, disabling two-factor authentication
        /// and requiring the user to set up 2FA again. This also invalidates
        /// all existing recovery codes.
        /// </remarks>
        /// <returns>Operation result</returns>
        /// <response code="200">Authenticator reset</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapPost(Authentication.ResetAuthenticator.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ResetAuthenticatorRequest, Response>(new ResetAuthenticatorRequest())
            .Response())
            .WithDocumentation(Authentication.ResetAuthenticator);

        /// <summary>
        /// [POST] /authentication/set-password - Sets initial password
        /// </summary>
        /// <remarks>
        /// Allows users who registered via external providers to set a local
        /// password for their account. Only works if no password is currently set.
        /// </remarks>
        /// <param name="request">New password details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Password set successfully</response>
        /// <response code="400">Password already exists or invalid request</response>
        routeGroup.MapPost(Authentication.SetPassword.Endpoint,
            IResult (IMediator mediator, SetPasswordRequest request) => mediator
            .HandleAsync<SetPasswordRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.SetPassword);

        /// <summary>
        /// [POST] /authentication/change-password - Updates password
        /// </summary>
        /// <remarks>
        /// Changes the user's account password. Requires verification of
        /// the current password.
        /// </remarks>
        /// <param name="request">Password change details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Password changed</response>
        /// <response code="400">Current password incorrect</response>
        routeGroup.MapPost(Authentication.ChangePassword.Endpoint,
            IResult (IMediator mediator, ChangePasswordRequest request) => mediator
            .HandleAsync<ChangePasswordRequest, Response>(request)
            .Response())
            .WithDocumentation(Authentication.ChangePassword);

        /// <summary>
        /// [GET] /authentication/account-status - Gets security status
        /// </summary>
        /// <remarks>
        /// Returns the current security configuration of the user's account,
        /// including 2FA status, external logins, and password information.
        /// </remarks>
        /// <returns>Account security status</returns>
        /// <response code="200">Returns account status</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapGet(Authentication.AccountStatus.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<AccountStatusRequest, Response>(new AccountStatusRequest())
            .Response())
            .WithDocumentation(Authentication.AccountStatus);

        /// <summary>
        /// [DELETE] /authentication/forgot-browser - Revokes browser remember
        /// </summary>
        /// <remarks>
        /// Revokes the "remember this browser" status for the current browser,
        /// requiring two-factor authentication on next login from this device.
        /// </remarks>
        /// <returns>Operation result</returns>
        /// <response code="204">Browser forgotten</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapDelete(Authentication.ForgotBrowser.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<ForgotBrowserRequest, Response>(new ForgotBrowserRequest())
            .Response())
            .WithDocumentation(Authentication.ForgotBrowser);

        /// <summary>
        /// [POST] /authentication/disable-2fa - Turns off two-factor auth
        /// </summary>
        /// <remarks>
        /// Disables two-factor authentication for the user account. This
        /// reduces account security and is not recommended.
        /// </remarks>
        /// <returns>Operation result</returns>
        /// <response code="200">2FA disabled</response>
        /// <response code="400">2FA not enabled</response>
        routeGroup.MapPost(Authentication.Disable2fa.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<Disable2faRequest, Response>(new Disable2faRequest())
            .Response())
            .WithDocumentation(Authentication.Disable2fa);
    }
}
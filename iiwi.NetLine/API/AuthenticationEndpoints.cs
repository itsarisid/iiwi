using iiwi.Application.Authentication;
using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

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
public class AuthenticationEndpoints : IEndpoint
{
    /// <summary>
    /// Registers all authentication-related endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        // Create an authorized route group for all endpoints
        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(AuthenticationDoc.Group)
            .RequireAuthorization()
            .AddEndpointFilter<ExceptionHandlingFilter>();

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
        routeGroup.MapVersionedEndpoint(new Configure<LoadKeyAndQrCodeUriRequest, LoadKeyAndQrCodeUriResponse>
        {
            EndpointDetails = AuthenticationDoc.LoadKeyAndQrCodeUri,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.LoadKeyAndQrCodeUri],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<EnableAuthenticatorRequest, EnableAuthenticatorResponse>
        {
            EndpointDetails = AuthenticationDoc.EnableAuthenticator,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.EnableAuthenticator],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<ExternalLoginsRequest, ExternalLoginsResponse>
        {
            EndpointDetails = AuthenticationDoc.ExternalLogins,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.ExternalLogins],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<RemoveLoginRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.RemoveLogin,
            HttpMethod = HttpVerb.Delete,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.RemoveLogin],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<LinkLoginRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.LinkLogin,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.LinkLogin],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<LinkLoginCallbackRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.LinkLoginCallback,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.LinkLoginCallback],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<GenerateRecoveryCodesRequest, GenerateRecoveryCodesResponse>
        {
            EndpointDetails = AuthenticationDoc.GenerateRecoveryCodes,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.GenerateRecoveryCodes],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<ResetAuthenticatorRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.ResetAuthenticator,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.ResetAuthenticator],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<SetPasswordRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.SetPassword,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.SetPassword],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<ChangePasswordRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.ChangePassword,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.ChangePassword],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<AccountStatusRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.AccountStatus,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.AccountStatus],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<ForgotBrowserRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.ForgotBrowser,
            HttpMethod = HttpVerb.Delete,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.ForgotBrowser],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

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
        routeGroup.MapVersionedEndpoint(new Configure<Disable2faRequest, Response>
        {
            EndpointDetails = AuthenticationDoc.Disable2fa,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authentication.Disable2fa],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}

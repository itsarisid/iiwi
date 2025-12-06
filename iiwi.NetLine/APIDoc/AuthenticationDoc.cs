using iiwi.Model;
using iiwi.Model.Endpoints;
namespace iiwi.NetLine.APIDoc;

/// <summary>
/// Contains endpoints for authentication management and security features
/// </summary>
/// <remarks>
/// This group handles all authentication-related operations including:
/// <list type="bullet">
/// <item><description>Two-factor authentication setup and management</description></item>
/// <item><description>External login provider integration</description></item>
/// <item><description>Password management</description></item>
/// <item><description>Account security status</description></item>
/// </list>
/// All endpoints require authentication unless noted otherwise.
/// </remarks>
public static class AuthenticationDoc
{
    /// <summary>
    /// Group information for Authentication endpoints
    /// </summary>
    /// <remarks>
    /// Collection of endpoints that manage:
    /// <list type="bullet">
    /// <item><description>Multi-factor authentication configuration</description></item>
    /// <item><description>External identity provider integration</description></item>
    /// <item><description>Password security controls</description></item>
    /// <item><description>Account security status</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new()
    {
        Name = "Authentication",
        Tags = "Authentication",
        Summary = "Auth Management",
        Description = "Endpoints for managing authentication methods and security settings."
    };

    /// <summary>
    /// Retrieves authenticator setup data
    /// </summary>
    /// <remarks>
    /// Returns all required information for setting up an authenticator app:
    /// <list type="bullet">
    /// <item><description>Shared secret key</description></item>
    /// <item><description>QR code URI for quick setup</description></item>
    /// <item><description>Manual entry code</description></item>
    /// </list>
    /// This is the first step in enabling two-factor authentication.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails LoadKeyAndQrCodeUri => new()
    {
        Endpoint = "/load-key",
        Name = "Get Authenticator Setup",
        Summary = "Get 2FA setup data",
        Description = "Retrieves secret key and QR code for authenticator app setup."
    };

    /// <summary>
    /// Activates two-factor authentication
    /// </summary>
    /// <remarks>
    /// Completes 2FA setup by:
    /// <list type="bullet">
    /// <item><description>Verifying a test code from authenticator app</description></item>
    /// <item><description>Persisting the 2FA configuration</description></item>
    /// <item><description>Generating initial recovery codes</description></item>
    /// </list>
    /// After successful activation, 2FA will be required at next login.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails EnableAuthenticator => new()
    {
        Endpoint = "/enable-authenticator",
        Name = "Enable 2FA",
        Summary = "Activate authenticator",
        Description = "Verifies authenticator code and enables two-factor authentication."
    };

    /// <summary>
    /// Lists connected external logins
    /// </summary>
    /// <remarks>
    /// Returns all external identity providers currently linked to the account:
    /// <list type="bullet">
    /// <item><description>Provider names (Google, Facebook, etc.)</description></item>
    /// <item><description>Provider keys</description></item>
    /// <item><description>Link creation dates</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ExternalLogins => new()
    {
        Endpoint = "/external-logins",
        Name = "Get External Logins",
        Summary = "List connected logins",
        Description = "Retrieves all linked external authentication providers."
    };

    /// <summary>
    /// Removes external login connection
    /// </summary>
    /// <remarks>
    /// Unlinks specified external provider from user account.
    /// <para>
    /// Important considerations:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Cannot remove last login method if no password set</description></item>
    /// <item><description>May require alternative login setup</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails RemoveLogin => new()
    {
        Endpoint = "/remove-login",
        Name = "Remove External Login",
        Summary = "Unlink provider",
        Description = "Disconnects an external identity provider from the account."
    };

    /// <summary>
    /// Initiates external login connection
    /// </summary>
    /// <remarks>
    /// Begins OAuth flow to link new external provider:
    /// <list type="bullet">
    /// <item><description>Redirects to provider's auth page</description></item>
    /// <item><description>Requires callback to complete</description></item>
    /// </list>
    /// Typically followed by <see cref="LinkLoginCallback"/>.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails LinkLogin => new()
    {
        Endpoint = "/link-login",
        Name = "Link External Login",
        Summary = "Connect new provider",
        Description = "Initiates linking of a new external authentication provider."
    };

    /// <summary>
    /// Completes external login connection
    /// </summary>
    /// <remarks>
    /// Finalizes OAuth flow to link external provider.
    /// <para>
    /// Handles:
    /// </para>
    /// <list type="bullet">
    /// <item><description>OAuth callback from provider</description></item>
    /// <item><description>Token validation</description></item>
    /// <item><description>Account linking</description></item>
    /// </list>
    /// Requires prior initiation via <see cref="LinkLogin"/>.
    /// </remarks>
    public static EndpointDetails LinkLoginCallback => new()
    {
        Endpoint = "/link-login-callback",
        Name = "External Login Callback",
        Summary = "Complete provider link",
        Description = "Finalizes linking of external authentication provider."
    };

    /// <summary>
    /// Creates new 2FA recovery codes
    /// </summary>
    /// <remarks>
    /// Generates one-time use backup codes with:
    /// <list type="bullet">
    /// <item><description>10 new codes by default</description></item>
    /// <item><description>Single-use each</description></item>
    /// <item><description>Immediately invalidates old codes</description></item>
    /// </list>
    /// Users should store these securely.
    /// Requires authentication and 2FA setup.
    /// </remarks>
    public static EndpointDetails GenerateRecoveryCodes => new()
    {
        Endpoint = "/generate-recovery-codes",
        Name = "Generate Recovery Codes",
        Summary = "Create backup codes",
        Description = "Generates new set of two-factor authentication recovery codes."
    };

    /// <summary>
    /// Resets authenticator configuration
    /// </summary>
    /// <remarks>
    /// Clears current 2FA setup by:
    /// <list type="bullet">
    /// <item><description>Invalidating authenticator key</description></item>
    /// <item><description>Disabling 2FA</description></item>
    /// <item><description>Invalidating recovery codes</description></item>
    /// </list>
    /// Used when switching authenticator devices or apps.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ResetAuthenticator => new()
    {
        Endpoint = "/reset-authenticator",
        Name = "Reset Authenticator",
        Summary = "Reset 2FA setup",
        Description = "Clears current authenticator configuration and disables 2FA."
    };

    /// <summary>
    /// Establishes local account password
    /// </summary>
    /// <remarks>
    /// Creates password for accounts that:
    /// <list type="bullet">
    /// <item><description>Were created via external login</description></item>
    /// <item><description>Need local authentication method</description></item>
    /// </list>
    /// Enables login without external provider.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails SetPassword => new()
    {
        Endpoint = "/set-password",
        Name = "Set Password",
        Summary = "Create password",
        Description = "Establishes local account password for external login accounts."
    };

    /// <summary>
    /// Updates account password
    /// </summary>
    /// <remarks>
    /// Changes existing password after verifying:
    /// <list type="bullet">
    /// <item><description>Current password</description></item>
    /// <item><description>New password meets complexity rules</description></item>
    /// </list>
    /// Invalidates all refresh tokens after change.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "Update password",
        Description = "Modifies the account's authentication password."
    };

    /// <summary>
    /// Retrieves security status
    /// </summary>
    /// <remarks>
    /// Returns current authentication configuration:
    /// <list type="bullet">
    /// <item><description>2FA enabled status</description></item>
    /// <item><description>Linked external providers</description></item>
    /// <item><description>Password set status</description></item>
    /// <item><description>Recovery codes remaining</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails AccountStatus => new()
    {
        Endpoint = "/account-status",
        Name = "Get Security Status",
        Summary = "View auth settings",
        Description = "Retrieves current authentication configuration and status."
    };

    /// <summary>
    /// Revokes browser trust
    /// </summary>
    /// <remarks>
    /// Removes "remember this browser" status by:
    /// <list type="bullet">
    /// <item><description>Clearing browser cookie</description></item>
    /// <item><description>Requiring 2FA on next login</description></item>
    /// </list>
    /// Used when accessing account from shared or public devices.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ForgotBrowser => new()
    {
        Endpoint = "/forgot-browser",
        Name = "Forget Browser",
        Summary = "Revoke browser trust",
        Description = "Removes trusted status from current browser for 2FA purposes."
    };

    /// <summary>
    /// Deactivates two-factor authentication
    /// </summary>
    /// <remarks>
    /// Turns off 2FA protection by:
    /// <list type="bullet">
    /// <item><description>Clearing authenticator key</description></item>
    /// <item><description>Invalidating recovery codes</description></item>
    /// <item><description>Disabling 2FA requirement</description></item>
    /// </list>
    /// Requires password verification.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails Disable2fa => new()
    {
        Endpoint = "/disable-2fa",
        Name = "Disable 2FA",
        Summary = "Turn off two-factor",
        Description = "Completely disables two-factor authentication for the account."
    };
}
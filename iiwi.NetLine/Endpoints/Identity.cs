using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Contains endpoints for user identity and authentication management
/// </summary>
/// <remarks>
/// This group handles all identity-related operations including:
/// <list type="bullet">
/// <item><description>User registration and authentication</description></item>
/// <item><description>Password management and recovery</description></item>
/// <item><description>Email verification processes</description></item>
/// <item><description>Multi-factor authentication</description></item>
/// <item><description>Token management</description></item>
/// </list>
/// </remarks>
public static class Identity
{
    /// <summary>
    /// Group information for Identity endpoints
    /// </summary>
    /// <remarks>
    /// Collection of endpoints that manage the complete identity lifecycle:
    /// <list type="bullet">
    /// <item><description>Account creation and verification</description></item>
    /// <item><description>Authentication and session management</description></item>
    /// <item><description>Credential recovery and updates</description></item>
    /// <item><description>Account security features</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new()
    {
        Name = "Identity",
        Tags = "Identity",
        Summary = "Identity Management",
        Description = "Endpoints for user authentication, registration, and account management."
    };

    /// <summary>
    /// Creates a new user account
    /// </summary>
    /// <remarks>
    /// Accepts user registration details including:
    /// <list type="bullet">
    /// <item><description>Email address</description></item>
    /// <item><description>Password (meeting complexity requirements)</description></item>
    /// <item><description>Basic profile information</description></item>
    /// </list>
    /// Successful registration typically triggers:
    /// <list type="bullet">
    /// <item><description>Email verification process</description></item>
    /// <item><description>Welcome email</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Register => new()
    {
        Endpoint = "/register",
        Name = "Register",
        Summary = "Create account",
        Description = "Registers a new user account with the provided credentials and profile information."
    };

    /// <summary>
    /// Authenticates a user
    /// </summary>
    /// <remarks>
    /// Validates user credentials and returns:
    /// <list type="bullet">
    /// <item><description>Access token (JWT)</description></item>
    /// <item><description>Refresh token</description></item>
    /// <item><description>Authentication session details</description></item>
    /// </list>
    /// May trigger multi-factor authentication if enabled.
    /// </remarks>
    public static EndpointDetails Login => new()
    {
        Endpoint = "/login",
        Name = "Login",
        Summary = "Authenticate",
        Description = "Validates user credentials and returns authentication tokens."
    };

    /// <summary>
    /// Terminates the current session
    /// </summary>
    /// <remarks>
    /// Invalidates the current access token and:
    /// <list type="bullet">
    /// <item><description>Revokes refresh tokens</description></item>
    /// <item><description>Clears session cookies if used</description></item>
    /// <item><description>Logs the logout event</description></item>
    /// </list>
    /// Requires valid authentication.
    /// </remarks>
    public static EndpointDetails Logout => new()
    {
        Endpoint = "/logout",
        Name = "Logout",
        Summary = "End session",
        Description = "Terminates the current authentication session and invalidates tokens."
    };

    /// <summary>
    /// Completes password reset process
    /// </summary>
    /// <remarks>
    /// Accepts:
    /// <list type="bullet">
    /// <item><description>Password reset token</description></item>
    /// <item><description>New password</description></item>
    /// </list>
    /// Typically used after <see cref="ForgotPassword"/> flow.
    /// Invalidates all existing sessions after successful reset.
    /// </remarks>
    public static EndpointDetails ResetPassword => new()
    {
        Endpoint = "/reset-password",
        Name = "Reset Password",
        Summary = "Reset password",
        Description = "Completes password reset using a valid reset token."
    };

    /// <summary>
    /// Initiates password recovery
    /// </summary>
    /// <remarks>
    /// Triggers password reset workflow:
    /// <list type="bullet">
    /// <item><description>Verifies email exists</description></item>
    /// <item><description>Generates reset token</description></item>
    /// <item><description>Sends reset instructions</description></item>
    /// </list>
    /// Reset links typically expire after 24 hours.
    /// </remarks>
    public static EndpointDetails ForgotPassword => new()
    {
        Endpoint = "/forgot-password",
        Name = "Forgot Password",
        Summary = "Request password reset",
        Description = "Initiates password recovery process via email."
    };

    /// <summary>
    /// Updates authenticated user's password
    /// </summary>
    /// <remarks>
    /// Requires:
    /// <list type="bullet">
    /// <item><description>Current password</description></item>
    /// <item><description>New password</description></item>
    /// </list>
    /// Invalidates all other sessions after successful change.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "Update password",
        Description = "Changes password for authenticated user."
    };

    /// <summary>
    /// Verifies email address
    /// </summary>
    /// <remarks>
    /// Completes email verification process by:
    /// <list type="bullet">
    /// <item><description>Validating verification token</description></item>
    /// <item><description>Marking email as verified</description></item>
    /// <item><description>Updating account status</description></item>
    /// </list>
    /// Typically called from verification email link.
    /// </remarks>
    public static EndpointDetails VerifyEmail => new()
    {
        Endpoint = "/verify-email",
        Name = "Verify Email",
        Summary = "Confirm email",
        Description = "Completes email verification using a verification token."
    };

    /// <summary>
    /// Resends verification email
    /// </summary>
    /// <remarks>
    /// Re-sends email verification when:
    /// <list type="bullet">
    /// <item><description>Original email wasn't received</description></item>
    /// <item><description>Verification token expired</description></item>
    /// </list>
    /// Implements rate limiting to prevent abuse.
    /// </remarks>
    public static EndpointDetails ResendVerificationEmail => new()
    {
        Endpoint = "/resend-verification-email",
        Name = "Resend Verification Email",
        Summary = "Resend verification",
        Description = "Sends new email verification message."
    };

    /// <summary>
    /// Renews access token
    /// </summary>
    /// <remarks>
    /// Generates new access token when:
    /// <list type="bullet">
    /// <item><description>Current access token expired</description></item>
    /// <item><description>Valid refresh token provided</description></item>
    /// </list>
    /// May return new refresh token depending on configuration.
    /// </remarks>
    public static EndpointDetails Refresh => new()
    {
        Endpoint = "/refresh",
        Name = "Refresh",
        Summary = "Renew token",
        Description = "Generates new access token using refresh token."
    };
    /// <summary>
    /// Confirms a user's email address
    /// </summary>
    /// <remarks>
    /// Completes the email verification process by:
    /// <list type="bullet">
    /// <item><description>Validating the confirmation token</description></item>
    /// <item><description>Marking the email as verified in the system</description></item>
    /// <item><description>Updating the account's verification status</description></item>
    /// </list>
    /// Typically accessed via a link in the verification email.
    /// Tokens have a limited validity period (usually 24 hours).
    /// </remarks>
    public static EndpointDetails ConfirmEmail => new()
    {
        Endpoint = "/confirm-email",
        Name = "Confirm Email",
        Summary = "Verify email",
        Description = "Completes email verification using a valid confirmation token."
    };

    /// <summary>
    /// Resends the email confirmation message
    /// </summary>
    /// <remarks>
    /// Generates and sends a new verification email when:
    /// <list type="bullet">
    /// <item><description>The original email wasn't received</description></item>
    /// <item><description>The verification token expired</description></item>
    /// <item><description>The user requests a new verification link</description></item>
    /// </list>
    /// Implements rate limiting to prevent abuse (typically 1 email every 5 minutes).
    /// The new token invalidates any previous unexpired tokens.
    /// </remarks>
    public static EndpointDetails ResendConfirmationEmail => new()
    {
        Endpoint = "/resend-confirmation-email",
        Name = "Resend Confirmation Email",
        Summary = "Resend verification",
        Description = "Sends a new email confirmation message with fresh verification link."
    };

    /// <summary>
    /// Manages multi-factor authentication
    /// </summary>
    /// <remarks>
    /// Handles complete MFA lifecycle:
    /// <list type="bullet">
    /// <item><description>MFA setup and configuration</description></item>
    /// <item><description>Verification code validation</description></item>
    /// <item><description>Recovery code management</description></item>
    /// </list>
    /// Supports TOTP authenticator apps and SMS codes.
    /// </remarks>
    public static EndpointDetails MFA => new()
    {
        Endpoint = "/2fa",
        Name = "Multifactor Authentication",
        Summary = "Manage 2FA",
        Description = "Handles multi-factor authentication setup and verification."
    };

    /// <summary>
    /// Retrieves basic account info
    /// </summary>
    /// <remarks>
    /// Returns non-sensitive account information including:
    /// <list type="bullet">
    /// <item><description>User identifier</description></item>
    /// <item><description>Verified email status</description></item>
    /// <item><description>Account creation date</description></item>
    /// <item><description>MFA configuration status</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails Info => new()
    {
        Endpoint = "/info",
        Name = "Info",
        Summary = "Get account info",
        Description = "Returns basic information about authenticated user."
    };

    /// <summary>
    /// Updates account information
    /// </summary>
    /// <remarks>
    /// Allows modification of:
    /// <list type="bullet">
    /// <item><description>Profile information</description></item>
    /// <item><description>Contact details</description></item>
    /// <item><description>Notification preferences</description></item>
    /// </list>
    /// Sensitive changes may require re-authentication.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails Information => new()
    {
        Endpoint = "/info",
        Name = "Information",
        Summary = "Update account",
        Description = "Modifies user account information."
    };
}
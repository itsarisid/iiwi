using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Provides metadata for all identity-related API endpoints.
/// </summary>
public static class Identity
{
    /// <summary>
    /// Metadata for the Identity endpoint group.
    /// </summary>
    public static EndpointDetails Group => new()
    {
        Name = "Identity",
        Tags = "Identity",
        Summary = "Identity-related Endpoints",
        Description = "This group contains all the endpoints related to identity management, including login, register, reset-password."
    };

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    public static EndpointDetails Register => new()
    {
        Endpoint = "/register",
        Name = "Register",
        Summary = "Register a new user",
        Description = "Creates a new user account using the provided registration details, such as email and password."
    };

    /// <summary>
    /// Authenticates a user and returns access tokens.
    /// </summary>
    public static EndpointDetails Login => new()
    {
        Endpoint = "/login",
        Name = "Login",
        Summary = "User login",
        Description = "Authenticates a user using credentials (email and password) and returns a token if successful."
    };

    /// <summary>
    /// Logs out the authenticated user by revoking their token.
    /// </summary>
    public static EndpointDetails Logout => new()
    {
        Endpoint = "/logout",
        Name = "Logout",
        Summary = "User logout",
        Description = "Logs out the current user by invalidating the access token or session."
    };

    /// <summary>
    /// Initiates the reset password process for a user.
    /// </summary>
    public static EndpointDetails ResetPassword => new()
    {
        Endpoint = "/reset-password",
        Name = "Reset Password",
        Summary = "Reset a user's password",
        Description = "Allows a user to reset their password using a reset token sent to their email."
    };

    /// <summary>
    /// Sends a password reset email to the user.
    /// </summary>
    public static EndpointDetails ForgotPassword => new()
    {
        Endpoint = "/forgot-password",
        Name = "Forgot Password",
        Summary = "Request password reset",
        Description = "Initiates the password reset process by sending a reset link to the user's email."
    };

    /// <summary>
    /// Allows a user to change their password while logged in.
    /// </summary>
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "Change current password",
        Description = "Enables a logged-in user to change their password by providing the current and new password."
    };

    /// <summary>
    /// Verifies a user's email address.
    /// </summary>
    public static EndpointDetails VerifyEmail => new()
    {
        Endpoint = "/verify-email",
        Name = "Verify Email",
        Summary = "Verify user email",
        Description = "Verifies the user's email address using a verification token sent to their email."
    };

    /// <summary>
    /// Resends a verification email to the user.
    /// </summary>
    public static EndpointDetails ResendVerificationEmail => new()
    {
        Endpoint = "/resend-verification-email",
        Name = "Resend Verification Email",
        Summary = "Resend email verification",
        Description = "Resends an email verification link to the user if they did not receive or use the original one."
    };

    /// <summary>
    /// Refreshes the user's access token.
    /// </summary>
    public static EndpointDetails Refresh => new()
    {
        Endpoint = "/refresh",
        Name = "Refresh",
        Summary = "Refresh access token",
        Description = "Generates a new access token using a valid refresh token."
    };

    /// <summary>
    /// Confirms a user's email address (alias for verify).
    /// </summary>
    public static EndpointDetails ConfirmEmail => new()
    {
        Endpoint = "/confirm-email",
        Name = "Confirm Email",
        Summary = "Confirm email address",
        Description = "Confirms the email address of a user using a confirmation token."
    };

    /// <summary>
    /// Resends the confirmation email to the user.
    /// </summary>
    public static EndpointDetails ResendConfirmationEmail => new()
    {
        Endpoint = "/resend-confirmation-email",
        Name = "Resend Confirmation Email",
        Summary = "Resend email confirmation",
        Description = "Sends a new confirmation email to the user if the original was not received or used."
    };

    /// <summary>
    /// Initiates or verifies multi-factor authentication.
    /// </summary>
    public static EndpointDetails MFA => new()
    {
        Endpoint = "/2fa",
        Name = "Multifactor Authentication",
        Summary = "Two-factor authentication (2FA)",
        Description = "Handles multi-factor authentication using a one-time code or authenticator app."
    };

    /// <summary>
    /// Provides basic user account info.
    /// </summary>
    public static EndpointDetails Info => new()
    {
        Endpoint = "/info",
        Name = "Info",
        Summary = "Get user info",
        Description = "Returns basic account information for the authenticated user."
    };

    /// <summary>
    /// Update user account info.
    /// </summary>
    public static EndpointDetails Information => new()
    {
        Endpoint = "/info",
        Name = "Information",
        Summary = "Update user information",
        Description = "Update user account information for the authenticated user."
    };
}

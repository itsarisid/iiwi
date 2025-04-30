using iiwi.Model;

public static class Authentication
{
    /// <summary>
    /// Metadata for the Authentication group.
    /// </summary>
    public static EndpointDetails Group => new()
    {
        Name = "Authentication",
        Tags = "Authentication",
        Summary = "Authentication Endpoints",
        Description = "This group contains all the endpoints related to user authentication, login providers, two-factor auth, and security settings."
    };

    /// <summary>
    /// Returns the key and QR code URI for setting up authenticator apps.
    /// </summary>
    public static EndpointDetails LoadKeyAndQrCodeUri => new()
    {
        Endpoint = "/load-key",
        Name = "Load Key and QR Code URI",
        Summary = "Returns the key and QR code URI for setting up authenticator apps.",
        Description = "Used during 2FA setup to get the secret key and QR code URI required for apps like Google Authenticator."
    };

    /// <summary>
    /// Finalizes 2FA setup using a code from the authenticator app.
    /// </summary>
    public static EndpointDetails EnableAuthenticator => new()
    {
        Endpoint = "/enable-authenticator",
        Name = "Enable Authenticator",
        Summary = "Enables two-factor authentication for the current user.",
        Description = "Verifies a code from the authenticator app and enables 2FA for the user's account."
    };

    /// <summary>
    /// Lists external login providers linked to the user's account.
    /// </summary>
    public static EndpointDetails ExternalLogins => new()
    {
        Endpoint = "/external-logins",
        Name = "External Logins",
        Summary = "Lists external login providers linked to the user account.",
        Description = "Returns a list of OAuth-based logins (e.g., Google, Facebook) associated with the user."
    };

    /// <summary>
    /// Removes a linked external login from the user's account.
    /// </summary>
    public static EndpointDetails RemoveLogin => new()
    {
        Endpoint = "/remove-login",
        Name = "Remove Login",
        Summary = "Removes a linked external login from the user account.",
        Description = "Unlinks an external provider from the user's account."
    };

    /// <summary>
    /// Starts the linking process for an external login.
    /// </summary>
    public static EndpointDetails LinkLogin => new()
    {
        Endpoint = "/link-login",
        Name = "Link External Login",
        Summary = "Initiates the linking of an external login provider.",
        Description = "Redirects to the provider's auth page to link a new login to the user account."
    };

    /// <summary>
    /// Callback endpoint for completing external login linking.
    /// </summary>
    public static EndpointDetails LinkLoginCallback => new()
    {
        Endpoint = "/link-login-callback",
        Name = "Link Login Callback",
        Summary = "Callback endpoint for external login linking.",
        Description = "Finalizes the linking of an external login after successful authentication."
    };

    /// <summary>
    /// Generates one-time recovery codes for 2FA recovery.
    /// </summary>
    public static EndpointDetails GenerateRecoveryCodes => new()
    {
        Endpoint = "/generate-recovery-codes",
        Name = "Generate Recovery Codes",
        Summary = "Generates backup codes for 2FA recovery.",
        Description = "Provides a list of single-use recovery codes to regain access if 2FA is lost. Should be stored securely."
    };

    /// <summary>
    /// Resets the current authenticator key, disabling 2FA.
    /// </summary>
    public static EndpointDetails ResetAuthenticator => new()
    {
        Endpoint = "/reset-authenticator",
        Name = "Reset Authenticator",
        Summary = "Resets the authenticator key for 2FA.",
        Description = "Disables 2FA and clears the old key. Use this if changing devices or switching to a different app."
    };

    /// <summary>
    /// Sets a new password for the user (typically used after external login setup).
    /// </summary>
    public static EndpointDetails SetPassword => new()
    {
        Endpoint = "/set-password",
        Name = "Set Password",
        Summary = "Sets a new password for the user.",
        Description = "Allows password creation for accounts created via external login methods."
    };

    /// <summary>
    /// Allows the user to change their current password.
    /// </summary>
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "Changes the user’s password.",
        Description = "Lets the user update their account password. Typically used after login, or during a security update."
    };

    /// <summary>
    /// Returns current authentication and security settings of the user.
    /// </summary>
    public static EndpointDetails AccountStatus => new()
    {
        Endpoint = "/account-status",
        Name = "Account Status",
        Summary = "Returns the current authentication status of the account.",
        Description = "Indicates whether 2FA is enabled, external logins exist, and other auth details."
    };

    /// <summary>
    /// Removes the 'trusted' status from the current browser for 2FA.
    /// </summary>
    public static EndpointDetails ForgotBrowser => new()
    {
        Endpoint = "/forgot-browser",
        Name = "Forget Browser",
        Summary = "Removes trusted status from the current browser.",
        Description = "Forces the browser to require 2FA verification at the next login, even if previously trusted."
    };

    /// <summary>
    /// Disables two-factor authentication for the user's account.
    /// </summary>
    public static EndpointDetails Disable2fa => new()
    {
        Endpoint = "/disable-2fa",
        Name = "Disable Two-Factor Authentication",
        Summary = "Disables two-factor authentication for the user account.",
        Description = "Deactivates 2FA protection. Only available to verified users."
    };
}

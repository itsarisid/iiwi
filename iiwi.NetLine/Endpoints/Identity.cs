using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

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
        Description = "This group contains all the endpoints related to identity management, including login, register, reset-password"
    };

    public static EndpointDetails Register => new()
    {
        Endpoint = "/register",
        Name = "Register",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails Login => new()
    {
        Endpoint = "/login",
        Name = "Login",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails Logout => new()
    {
        Endpoint = "/logout",
        Name = "Logout",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails ResetPassword => new()
    {
        Endpoint = "/reset-password",
        Name = "Reset Password",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails ForgotPassword => new()
    {
        Endpoint = "/forgot-password",
        Name = "Forgot Password",
        Summary = "",
        Description = ""
    };
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails VerifyEmail => new()
    {
        Endpoint = "/verify-email",
        Name = "Verify Email",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails ResendVerificationEmail => new()
    {
        Endpoint = "/resend-verification-email",
        Name = "Resend Verification Email",
        Summary = "",
        Description = ""
    };
    public static EndpointDetails Refresh => new()
    {
        Endpoint = "/refresh",
        Name = "Refresh",
        Summary = "",
        Description = ""
    };
    public static EndpointDetails ConfirmEmail => new()
    {
        Endpoint = "/confirmEmail",
        Name = "Confirm Email",
        Summary = "",
        Description = ""
    };
    public static EndpointDetails ResendConfirmationEmail => new()
    {
        Endpoint = "/resend-confirmation-email",
        Name = "Resend Confirmation Email",
        Summary = "",
        Description = ""
    };
    public static EndpointDetails MFA => new ()
    {
        Endpoint = "/2fa",
        Name = "Multifactor Authentication",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails Info => new ()
    {
        Endpoint = "/info",
        Name = "Info",
        Summary = "",
        Description = ""
    };

}

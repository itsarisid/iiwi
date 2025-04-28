using iiwi.Model;

public static class Authentication
{
    public static EndpointDetails Group => new()
    {
        Name = "Authentication",
        Tags = "Authentication",
        Summary = "Authentication",
        Description = "This group contains all the endpoints related to authentication."
    };

    public static EndpointDetails LoadKeyAndQrCodeUri => new()
    {
        Endpoint = "/load-key",
        Name = "LoadKey And QrCode Uri",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails EnableAuthenticator => new()
    {
        Endpoint = "/enable-authenticator",
        Name = "Enable Authenticator",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails ExternalLogins => new()
    {
        Endpoint = "/external-logins",
        Name = "External Logins",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails RemoveLogin => new()
    {
        Endpoint = "/remove-login",
        Name = "Remove Login",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails LinkLogin => new()
    {
        Endpoint = "/link-login",
        Name = "Link Login",
        Summary = "",
        Description = ""
    };

    public static EndpointDetails LinkLoginCallback => new()
    {
        Endpoint = "/link-login-callback",
        Name = "Link Login Callback",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails GenerateRecoveryCodes => new()
    {
        Endpoint = "/generate-recovery-codes",
        Name = "Generate Recovery Codes",
        Summary = "Put these codes in a safe place. If you lose your device and don't have the recovery codes you will lose access to your account. Generating new recovery codes does not change the keys used in authenticator apps. If you wish to change the key used in an authenticator app you should reset your authenticator keys.",
        Description = ""
    };
    
    public static EndpointDetails ResetAuthenticator => new()
    {
        Endpoint = "/reset-authenticator",
        Name = "Reset Authenticator",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails SetPassword => new()
    {
        Endpoint = "/set-password",
        Name = "Set Password",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails AccountStatus => new()
    {
        Endpoint = "/account-status",
        Name = "AccountStatus",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails ForgotBrowser => new()
    {
        Endpoint = "/forgot-browser",
        Name = "Forgot Browser",
        Summary = "",
        Description = ""
    };
    
    public static EndpointDetails Disable2fa => new()
    {
        Endpoint = "/disable-2fa",
        Name = "Disable 2fa",
        Summary = "",
        Description = ""
    };


}
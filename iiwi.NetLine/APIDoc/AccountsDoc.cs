using iiwi.Model;

namespace iiwi.NetLine.APIDoc;

public class AccountsDoc
{
    public static EndpointDetails Group => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Account Management",
        Description = "Endpoints for managing user profiles, contact information, and personal data."
    };

    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "Modify profile",
        Description = "Updates personal information in the user's profile."
    };

    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Email",
        Summary = "Request verification",
        Description = "Initiates email verification process for the account."
    };

    public static EndpointDetails DownloadPersonalData => new()
    {
        Endpoint = "/download-personal-data",
        Name = "Download Personal Data",
        Summary = "Export user data",
        Description = "Generates downloadable archive of all personal data."
    };

    public static EndpointDetails ChangeEmail => new()
    {
        Endpoint = "/change-email",
        Name = "Change Email",
        Summary = "Update email",
        Description = "Changes account's primary email address with verification."
    };

    public static EndpointDetails DeletePersonalData => new()
    {
        Endpoint = "/delete-personal-data",
        Name = "Delete Personal Data",
        Summary = "Erase account data",
        Description = "Permanently removes personally identifiable information."
    };

    public static EndpointDetails UpdatePhoneNumber => new()
    {
        Endpoint = "/update-phone-number",
        Name = "Update Phone Number",
        Summary = "Change phone",
        Description = "Updates verified phone number associated with account."
    };
}

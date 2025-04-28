using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Represents all account-related endpoint details.
/// </summary>
public static class Accounts
{
    /// <summary>
    /// General information about the Accounts endpoint group.
    /// </summary>
    public static EndpointDetails Group => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Account Management APIs",
        Description = "This group contains all the endpoints related to user account management tasks."
    };

    /// <summary>
    /// Endpoint to update a user's profile information.
    /// </summary>
    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "Update user profile information",
        Description = "Allows a user to update their profile details such as name, address, and other personal information."
    };

    /// <summary>
    /// Endpoint to send a verification email to the user's registered address.
    /// </summary>
    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Email",
        Summary = "Send email verification",
        Description = "Sends a verification email to confirm the user's email address."
    };

    /// <summary>
    /// Endpoint to download all personal data associated with a user's account.
    /// </summary>
    public static EndpointDetails DownloadPersonalData => new()
    {
        Endpoint = "/download-personal-data",
        Name = "Download Personal Data",
        Summary = "Export user's personal data",
        Description = "Allows users to download a copy of all their personal data stored in the system."
    };

    /// <summary>
    /// Endpoint to change the user's registered email address.
    /// </summary>
    public static EndpointDetails ChangeEmail => new()
    {
        Endpoint = "/change-email",
        Name = "Change Email",
        Summary = "Update registered email address",
        Description = "Allows a user to update or change their primary email address linked to their account."
    };

    /// <summary>
    /// Endpoint to permanently delete a user's personal data.
    /// </summary>
    public static EndpointDetails DeletePersonalData => new()
    {
        Endpoint = "/delete-personal-data",
        Name = "Delete Personal Data",
        Summary = "Erase user's personal information",
        Description = "Deletes all stored personal data associated with the user's account, following GDPR or privacy requirements."
    };

    /// <summary>
    /// Endpoint to change the user's account password.
    /// </summary>
    public static EndpointDetails ChangePassword => new()
    {
        Endpoint = "/change-password",
        Name = "Change Password",
        Summary = "Update account password",
        Description = "Allows a user to update their password for improved security or after a password reset request."
    };

    /// <summary>
    /// Endpoint to change the user's phone number.
    /// </summary>
    public static EndpointDetails UpdatePhoneNumber => new()
    {
        Endpoint = "/update-phone-number",
        Name = "Update Phone Number",
        Summary = "Update Phone Number",
        Description = "Allows a user to update their phone number."
    };
}

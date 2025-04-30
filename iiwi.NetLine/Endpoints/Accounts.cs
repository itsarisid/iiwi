using iiwi.Model;

public static class Accounts
{
    /// <summary>
    /// Metadata for the Accounts endpoint group.
    /// </summary>
    public static EndpointDetails Group => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Account-related Endpoints",
        Description = "This group contains all the endpoints related to user account management, including profile updates, email changes, and personal data handling."
    };

    /// <summary>
    /// Updates the profile information of a user.
    /// </summary>
    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "Updates the user profile data.",
        Description = "Allows a user to modify personal details such as name, contact info, or preferences stored in their profile."
    };

    /// <summary>
    /// Sends a verification email to the user’s registered email address.
    /// </summary>
    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Details",
        Summary = "Sends a verification email to the user.",
        Description = "Used to verify a user's email address during registration or when changing an email. Helps ensure the address belongs to the user."
    };

    /// <summary>
    /// Allows the user to download their stored personal data.
    /// </summary>
    public static EndpointDetails DownloadPersonalData => new()
    {
        Endpoint = "/download-personal-data",
        Name = "Download Personal Data",
        Summary = "Exports the user's personal data.",
        Description = "Provides a downloadable copy of the personal data the system has stored about the user, to support GDPR or similar policies."
    };

    /// <summary>
    /// Initiates the process of changing the user's email address.
    /// </summary>
    public static EndpointDetails ChangeEmail => new()
    {
        Endpoint = "/change-email",
        Name = "Change Email",
        Summary = "Changes the user’s registered email address.",
        Description = "Updates the email address on file. May require confirmation via a verification link sent to the new address."
    };

    /// <summary>
    /// Permanently deletes the user’s personal data from the system.
    /// </summary>
    public static EndpointDetails DeletePersonalData => new()
    {
        Endpoint = "/delete-personal-data",
        Name = "Delete Personal Data",
        Summary = "Deletes personal data associated with the account.",
        Description = "Removes all personally identifiable information (PII) related to the user. Often used for right-to-be-forgotten compliance."
    };

}

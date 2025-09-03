using iiwi.Model;

/// <summary>
/// Contains endpoints for user account management and personal data handling
/// </summary>
/// <remarks>
/// This group handles all account-related operations including:
/// <list type="bullet">
/// <item><description>Profile information management</description></item>
/// <item><description>Email and contact updates</description></item>
/// <item><description>Personal data export and deletion</description></item>
/// <item><description>Account verification processes</description></item>
/// </list>
/// All endpoints require authentication unless noted otherwise.
/// </remarks>
public static class Accounts
{
    /// <summary>
    /// Group information for Accounts endpoints
    /// </summary>
    /// <remarks>
    /// Collection of endpoints that manage user account details and personal data:
    /// <list type="bullet">
    /// <item><description>Profile updates and personal information</description></item>
    /// <item><description>Contact information changes</description></item>
    /// <item><description>Data privacy operations</description></item>
    /// <item><description>Account verification flows</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new()
    {
        Name = "Accounts",
        Tags = "Accounts",
        Summary = "Account Management",
        Description = "Endpoints for managing user profiles, contact information, and personal data."
    };

    /// <summary>
    /// Updates user profile information
    /// </summary>
    /// <remarks>
    /// Modifies personal details including:
    /// <list type="bullet">
    /// <item><description>First and last name</description></item>
    /// <item><description>Profile picture</description></item>
    /// <item><description>Personal preferences</description></item>
    /// <item><description>Demographic information</description></item>
    /// </list>
    /// Changes take effect immediately.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails UpdateProfile => new()
    {
        Endpoint = "/update-profile",
        Name = "Update Profile",
        Summary = "Modify profile",
        Description = "Updates personal information in the user's profile."
    };

    /// <summary>
    /// Initiates email verification
    /// </summary>
    /// <remarks>
    /// Triggers verification workflow:
    /// <list type="bullet">
    /// <item><description>Generates verification token</description></item>
    /// <item><description>Sends email with confirmation link</description></item>
    /// <item><description>Sets expiration timeframe (typically 24 hours)</description></item>
    /// </list>
    /// Used during registration or email changes.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails SendVerificationDetails => new()
    {
        Endpoint = "/send-verification-email",
        Name = "Send Verification Email",
        Summary = "Request verification",
        Description = "Initiates email verification process for the account."
    };

    /// <summary>
    /// Exports personal data
    /// </summary>
    /// <remarks>
    /// Generates comprehensive data export including:
    /// <list type="bullet">
    /// <item><description>Account information</description></item>
    /// <item><description>Activity history</description></item>
    /// <item><description>Stored preferences</description></item>
    /// <item><description>Connected services</description></item>
    /// </list>
    /// Supports GDPR and privacy regulations.
    /// May take several minutes to prepare.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails DownloadPersonalData => new()
    {
        Endpoint = "/download-personal-data",
        Name = "Download Personal Data",
        Summary = "Export user data",
        Description = "Generates downloadable archive of all personal data."
    };

    /// <summary>
    /// Changes account email address
    /// </summary>
    /// <remarks>
    /// Updates primary email with verification flow:
    /// <list type="bullet">
    /// <item><description>Requires password confirmation</description></item>
    /// <item><description>Sends verification to new address</description></item>
    /// <item><description>Maintains old email until confirmed</description></item>
    /// </list>
    /// Security notifications sent to both addresses.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails ChangeEmail => new()
    {
        Endpoint = "/change-email",
        Name = "Change Email",
        Summary = "Update email",
        Description = "Changes account's primary email address with verification."
    };

    /// <summary>
    /// Deletes personal data
    /// </summary>
    /// <remarks>
    /// Permanently removes:
    /// <list type="bullet">
    /// <item><description>Personal identifiers</description></item>
    /// <item><description>Profile information</description></item>
    /// <item><description>Activity history</description></item>
    /// </list>
    /// May retain some data for legal compliance.
    /// Account becomes inaccessible after this operation.
    /// Requires password confirmation.
    /// </remarks>
    public static EndpointDetails DeletePersonalData => new()
    {
        Endpoint = "/delete-personal-data",
        Name = "Delete Personal Data",
        Summary = "Erase account data",
        Description = "Permanently removes personally identifiable information."
    };

    /// <summary>
    /// Updates phone number
    /// </summary>
    /// <remarks>
    /// Modifies account's verified phone number:
    /// <list type="bullet">
    /// <item><description>Requires SMS verification</description></item>
    /// <item><description>Used for 2FA and recovery</description></item>
    /// <item><description>Replaces previous number immediately</description></item>
    /// </list>
    /// Security notification sent to old number if available.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails UpdatePhoneNumber => new()
    {
        Endpoint = "/update-phone-number",
        Name = "Update Phone Number",
        Summary = "Change phone",
        Description = "Updates verified phone number associated with account."
    };
}
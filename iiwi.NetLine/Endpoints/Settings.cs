using iiwi.Model;
namespace iiwi.NetLine.Endpoints;
/// <summary>
/// Contains endpoints for managing user settings and preferences
/// </summary>
/// <remarks>
/// This group handles all user-specific configurations including UI preferences,
/// notification settings, and application behavior customizations.
/// </remarks>
public static class Settings
{
    /// <summary>
    /// Group information for Settings endpoints
    /// </summary>
    /// <remarks>
    /// Collections of endpoints that manage user-specific configurations including:
    /// <list type="bullet">
    /// <item><description>UI themes and layouts</description></item>
    /// <item><description>Notification preferences</description></item>
    /// <item><description>Application behavior settings</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new EndpointDetails
    {
        Name = "Settings",
        Tags = "Settings",
        Summary = "User Settings",
        Description = "Endpoints for managing user preferences, notifications, and configurations."
    };

    /// <summary>
    /// Retrieves the current user's settings
    /// </summary>
    /// <remarks>
    /// Returns a complete set of the authenticated user's saved preferences including:
    /// <list type="bullet">
    /// <item><description>UI theme and layout preferences</description></item>
    /// <item><description>Notification channel settings</description></item>
    /// <item><description>Application-specific toggles and flags</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails GetUserPreferences => new EndpointDetails
    {
        Endpoint = "/get-user-preferences",
        Name = "Get User Preferences",
        Summary = "Get user settings",
        Description = "Retrieves all saved preference settings for the authenticated user."
    };

    /// <summary>
    /// Updates user preferences
    /// </summary>
    /// <remarks>
    /// Accepts a partial or complete settings object to update the user's preferences.
    /// <para>
    /// Typical updates include:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Changing UI theme (light/dark/custom)</description></item>
    /// <item><description>Updating notification preferences</description></item>
    /// <item><description>Modifying default application behaviors</description></item>
    /// </list>
    /// Returns the updated preferences object upon success.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails UpdatePreferences => new EndpointDetails
    {
        Endpoint = "/update-preferences",
        Name = "Update Preferences",
        Summary = "Update user settings",
        Description = "Modifies and persists changes to the user's application preferences."
    };
}
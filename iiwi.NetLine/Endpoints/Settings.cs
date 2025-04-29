using iiwi.Model;

public static class Settings
{
    /// <summary>
    /// Metadata for the Settings endpoint group.
    /// </summary>
    public static EndpointDetails Group => new EndpointDetails
    {
        Name = "Settings",
        Tags = "Settings",
        Summary = "User Settings Endpoints",
        Description = "Endpoints related to user preferences, notifications, themes, and configurations."
    };

    /// <summary>
    /// Retrieves the current user's preference settings.
    /// </summary>
    public static EndpointDetails GetUserPreferences => new EndpointDetails
    {
        Endpoint = "/get-user-preferences",
        Name = "Get User Preferences",
        Summary = "Returns the user's saved preference settings.",
        Description = "Useful for restoring UI settings, toggles, and saved behaviors across sessions."
    };

    /// <summary>
    /// Updates the user’s settings and preferences.
    /// </summary>
    public static EndpointDetails UpdatePreferences => new EndpointDetails
    {
        Endpoint = "/update-preferences",
        Name = "Update Preferences",
        Summary = "Saves changes to the user’s settings.",
        Description = "Allows toggling notification preferences, themes, layouts, and other UX options."
    };
}

using iiwi.Model;

public static class Security
{
    /// <summary>
    /// Metadata for the Security endpoint group.
    /// </summary>
    public static EndpointDetails Group => new EndpointDetails
    {
        Name = "Security",
        Tags = "Security",
        Summary = "Security Management Endpoints",
        Description = "Handles account protection features like device management, sessions, and security logs."
    };

    /// <summary>
    /// Retrieves a list of all active sessions for the user.
    /// </summary>
    public static EndpointDetails GetActiveSessions => new EndpointDetails
    {
        Endpoint = "/get-active-sessions",
        Name = "Get Active Sessions",
        Summary = "Returns information about current login sessions.",
        Description = "Lists browser, location, and device info for each session. Supports session termination."
    };

    /// <summary>
    /// Ends a specific session for the user.
    /// </summary>
    public static EndpointDetails RevokeSession => new EndpointDetails
    {
        Endpoint = "/revoke-session",
        Name = "Revoke Session",
        Summary = "Ends an active session by session ID.",
        Description = "Allows the user to remotely log out from other devices or browsers."
    };

    /// <summary>
    /// Returns a list of recent security-related events.
    /// </summary>
    public static EndpointDetails GetSecurityLogs => new EndpointDetails
    {
        Endpoint = "/get-security-logs",
        Name = "Security Logs",
        Summary = "Shows recent account events for auditing.",
        Description = "Includes events like failed logins, password changes, device additions, and authenticator resets."
    };
}

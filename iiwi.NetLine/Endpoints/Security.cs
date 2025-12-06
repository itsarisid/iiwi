using iiwi.Model;
using iiwi.Model.Endpoints;
namespace iiwi.NetLine.Endpoints;
/// <summary>
/// Contains endpoints for security management and account protection
/// </summary>
/// <remarks>
/// This group handles all security-related operations including:
/// <list type="bullet">
/// <item><description>Session management and tracking</description></item>
/// <item><description>Security event auditing</description></item>
/// <item><description>Account protection features</description></item>
/// </list>
/// All endpoints require authentication.
/// </remarks>
public static class Security
{
    /// <summary>
    /// Group information for Security endpoints
    /// </summary>
    /// <remarks>
    /// Collection of endpoints that manage account security features including:
    /// <list type="bullet">
    /// <item><description>Active session monitoring</description></item>
    /// <item><description>Session termination capabilities</description></item>
    /// <item><description>Security event logging</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new EndpointDetails
    {
        Name = "Security",
        Tags = "Security",
        Summary = "Account Security",
        Description = "Endpoints for managing sessions, security events, and account protection."
    };

    /// <summary>
    /// Retrieves active user sessions
    /// </summary>
    /// <remarks>
    /// Returns detailed information about all currently active sessions including:
    /// <list type="bullet">
    /// <item><description>Device type and browser information</description></item>
    /// <item><description>IP address and approximate location</description></item>
    /// <item><description>Session creation and last activity timestamps</description></item>
    /// </list>
    /// Essential for identifying suspicious activity.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails GetActiveSessions => new EndpointDetails
    {
        Endpoint = "/get-active-sessions",
        Name = "Get Active Sessions",
        Summary = "List active sessions",
        Description = "Retrieves all currently active authentication sessions for the user account."
    };

    /// <summary>
    /// Terminates a specific session
    /// </summary>
    /// <remarks>
    /// Allows forced logout from specific devices by session ID.
    /// <para>
    /// Typical use cases:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Terminating lost or stolen device sessions</description></item>
    /// <item><description>Ending suspicious or unrecognized sessions</description></item>
    /// <item><description>Remote logout from public/shared devices</description></item>
    /// </list>
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails RevokeSession => new EndpointDetails
    {
        Endpoint = "/revoke-session",
        Name = "Revoke Session",
        Summary = "Terminate session",
        Description = "Forcibly ends a specific authentication session by its unique identifier."
    };

    /// <summary>
    /// Retrieves security event history
    /// </summary>
    /// <remarks>
    /// Returns chronological log of security-related events including:
    /// <list type="bullet">
    /// <item><description>Successful and failed login attempts</description></item>
    /// <item><description>Password changes and resets</description></item>
    /// <item><description>Multi-factor authentication events</description></item>
    /// <item><description>Device authorization changes</description></item>
    /// </list>
    /// Useful for security auditing and identifying suspicious activity.
    /// Typically shows last 30-90 days of events.
    /// Requires authentication.
    /// </remarks>
    public static EndpointDetails GetSecurityLogs => new EndpointDetails
    {
        Endpoint = "/get-security-logs",
        Name = "Security Logs",
        Summary = "View security history",
        Description = "Retrieves chronological log of security events for the user account."
    };
}
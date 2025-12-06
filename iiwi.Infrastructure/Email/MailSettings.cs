namespace iiwi.Infrastructure.Email;

/// <summary>
/// Represents mail settings.
/// </summary>
public class MailSettings
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Mail { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the host.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the port.
    /// </summary>
    public int Port { get; set; }
}

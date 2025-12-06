
namespace iiwi.Domain.Logs;

/// <summary>
/// Represents a Serilog entry.
/// </summary>
public class Serilog
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the message template.
    /// </summary>
    public string MessageTemplate { get; set; }

    /// <summary>
    /// Gets or sets the log level.
    /// </summary>
    public string Level { get; set; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    public string Exception { get; set; }

    /// <summary>
    /// Gets or sets the properties.
    /// </summary>
    public string Properties { get; set; }
}

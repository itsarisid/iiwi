
namespace iiwi.Domain.Logs;

public class Serilog
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the Message.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Gets or sets the MessageTemplate.
    /// </summary>
    public string MessageTemplate { get; set; }
    /// <summary>
    /// Gets or sets the Level.
    /// </summary>
    public string Level { get; set; }
    /// <summary>
    /// Gets or sets the TimeStamp.
    /// </summary>
    public DateTime TimeStamp { get; set; }
    /// <summary>
    /// Gets or sets the Exception.
    /// </summary>
    public string Exception { get; set; }
    /// <summary>
    /// Gets or sets the Properties.
    /// </summary>
    public string Properties { get; set; }
}

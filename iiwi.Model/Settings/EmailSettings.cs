namespace iiwi.Model.Settings;

public class EmailSettings
{
    /// <summary>
    /// Gets or sets the TemplateName.
    /// </summary>
    public string TemplateName { get; set; }
    /// <summary>
    /// Gets or sets the Model.
    /// </summary>
    public object Model { get; set; }
    /// <summary>
    /// Gets or sets the Emails.
    /// </summary>
    public IList<string> Emails { get; set; }
    /// <summary>
    /// Gets or sets the Subject.
    /// </summary>
    public string Subject { get; set; }
}

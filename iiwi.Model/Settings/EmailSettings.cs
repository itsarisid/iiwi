namespace iiwi.Model.Settings;

/// <summary>
/// Represents email settings.
/// </summary>
public class EmailSettings
{
    /// <summary>
    /// Gets or sets the template name.
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    public object Model { get; set; }

    /// <summary>
    /// Gets or sets the emails.
    /// </summary>
    public IList<string> Emails { get; set; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; set; }
}

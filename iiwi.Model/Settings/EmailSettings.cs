namespace iiwi.Model.Settings;

public class EmailSettings
{
    public string TemplateName { get; set; }
    public object Model { get; set; }
    public IList<string> Emails { get; set; }
    public string Subject { get; set; }
}

using Microsoft.AspNetCore.Http;

namespace iiwi.Infrastructure.Email;

public class MailRequest
{
    /// <summary>
    /// Gets or sets the ToEmail.
    /// </summary>
    public string ToEmail { get; set; }
    /// <summary>
    /// Gets or sets the Subject.
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Gets or sets the Body.
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// Gets or sets the Attachments.
    /// </summary>
    public List<IFormFile> Attachments { get; set; }
}

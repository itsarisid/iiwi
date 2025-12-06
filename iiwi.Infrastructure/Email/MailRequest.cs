using Microsoft.AspNetCore.Http;

namespace iiwi.Infrastructure.Email;

/// <summary>
/// Represents a mail request.
/// </summary>
public class MailRequest
{
    /// <summary>
    /// Gets or sets the recipient email.
    /// </summary>
    public string ToEmail { get; set; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets the attachments.
    /// </summary>
    public List<IFormFile> Attachments { get; set; }
}

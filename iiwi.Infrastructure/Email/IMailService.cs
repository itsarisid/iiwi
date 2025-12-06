using iiwi.Model.Settings;

namespace iiwi.Infrastructure.Email;

/// <summary>
/// Interface for mail service.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="mailRequest">The mail request.</param>
    Task SendEmailAsync(MailRequest mailRequest);

    /// <summary>
    /// Sends an email with a template asynchronously.
    /// </summary>
    /// <param name="settings">The email settings.</param>
    Task SendEmailWithTemplateAsync(EmailSettings settings);
}

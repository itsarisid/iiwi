using iiwi.Model.Settings;

namespace iiwi.Infrastructure.Email;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
    Task SendEmailWithTemplateAsync(EmailSettings settings);
}

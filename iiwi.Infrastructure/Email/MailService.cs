using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using HtmlAgilityPack;
using iiwi.Model.Settings;
using System.Reflection;
using Fluid;
namespace iiwi.Infrastructure.Email;

/// <summary>
/// Service for sending emails.
/// </summary>
/// <param name="mailSettings">The mail settings.</param>
public class MailService(IOptions<MailSettings> mailSettings) : IMailService
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    /// <inheritdoc />
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail)
        };
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }

        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    /// <inheritdoc />
    public async Task SendEmailWithTemplateAsync(EmailSettings model)
    {
        //FIXME: Dynamic template path.
        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        string projectName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        var path = $"{solutiondir}\\iiwi\\{projectName}\\Templates\\{model.TemplateName}";
        var doc = new HtmlDocument();
        doc.Load(path);

        var parser = new FluidParser();

        List<Task> emailList = [];
        if (parser.TryParse(doc.Text, out var template, out var error))
        {
            var context = new TemplateContext(model.Model);
            var emailBody = await template.RenderAsync(context);

            foreach (var email in model.Emails)
            {
                var task = SendEmailAsync(new MailRequest
                {
                    Body = emailBody,
                    Subject = model.Subject,
                    ToEmail = email
                });

                emailList.Add(task);
            }
        }
        else
        {
            throw new Exception($"Error: {error}");
        }
        await Task.WhenAll([.. emailList]);
    }
}

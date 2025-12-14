using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LanguageApp.Services;

public class EmailService : IEmailSender
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();

        // REQUIRED
        message.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = htmlMessage
        }.ToMessageBody();

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _mailSettings.Host,
            _mailSettings.Port,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _mailSettings.Mail,
            _mailSettings.Password);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}

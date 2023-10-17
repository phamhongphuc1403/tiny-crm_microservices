using System.Text.Json;
using BuildingBlock.Application.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BuildingBlock.Infrastructure.MailKit;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(EmailSettings emailSettings, ILogger<EmailSender> logger)
    {
        _emailSettings = emailSettings;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
        _logger.LogInformation($"Email sent to {JsonSerializer.Serialize(message.ToEmails)}.");
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Support TinyCRM", _emailSettings.From));
        emailMessage.To.AddRange(message.ToEmails.Select(x => new MailboxAddress(string.Empty, x)));
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(mailMessage);
        }
        catch
        {
            _logger.LogError($"Email sending failed to {mailMessage.To}.");
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
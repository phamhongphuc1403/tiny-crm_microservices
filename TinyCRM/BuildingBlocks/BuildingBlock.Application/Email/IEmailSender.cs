namespace BuildingBlock.Application.Email;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}
using BuildingBlock.Application.Email;
using BuildingBlock.Infrastructure.MailKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.Extensions;

public static class EmailExtensions
{
    public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<EmailSettings>(_ =>
        {
            var emailSection = configuration.GetSection("EmailConfiguration");
            var emailSettings = new EmailSettings
            {
                From = emailSection["From"]!,
                Host = emailSection["SmtpServer"]!,
                Password = emailSection["Password"]!,
                Port = int.Parse(emailSection["Port"]!),
                Username = emailSection["Username"]!
            };
            return emailSettings;
        });

        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}
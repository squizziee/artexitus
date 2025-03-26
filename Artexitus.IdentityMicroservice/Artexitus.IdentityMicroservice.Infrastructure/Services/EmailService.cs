using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        private MimeMessage CreateMessage(User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.Username, _settings.From));
            emailMessage.To.Add(new MailboxAddress(user.Profile.Username, user.Email));
            emailMessage.Subject = "Account activation";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            { 
                Text = $"<a href='https://localhost:50000/api/account/activation?ActivationToken={user.ActivationToken}'>Press to activate account</a>"
            };

            return emailMessage;
        }

        public Task SendAccountActivationEmail(User user, CancellationToken cancellationToken)
        {
            var message = CreateMessage(user);

            using var client = new SmtpClient();

            client.Connect(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(_settings.From, _settings.Password);

            client.Send(message);
            client.Disconnect(true);

            return Task.CompletedTask;
        }
    }
}

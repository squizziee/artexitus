using Artexitus.IdentityMicroservice.Application.Services;
using Artexitus.IdentityMicroservice.Domain.Entities;
using Artexitus.IdentityMicroservice.Infrastructure.ConfigurationSections;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Artexitus.IdentityMicroservice.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        private MimeMessage CreateActivationMessage(User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.Username, _settings.From));
            emailMessage.To.Add(new MailboxAddress(user.Profile.Username, user.Email));
            emailMessage.Subject = "Account activation";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            { 
                Text = $"<a href='https://localhost:8081/api/account/activation?ActivationToken={user.ActivationToken}'>Press to activate account</a>"
            };

            return emailMessage;
        }

        private MimeMessage CreatePasswordResetMessage(User user, string passwordResetToken)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.Username, _settings.From));
            emailMessage.To.Add(new MailboxAddress(user.Profile.Username, user.Email));
            emailMessage.Subject = "Account activation";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<a href='https://localhost:8081/api/account/password-reset?PasswordResetToken={passwordResetToken}'>Press to enter password reset page</a>"
                // TODO should redirect to client page
            };

            return emailMessage;
        }

        public async Task SendAccountActivationEmail(User user, CancellationToken cancellationToken)
        {
            var message = CreateActivationMessage(user);

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _settings.SmtpServer, 
                _settings.Port,
                MailKit.Security.SecureSocketOptions.StartTls, 
                cancellationToken
            );
            await client.AuthenticateAsync(
                _settings.From, 
                _settings.Password, 
                cancellationToken
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true, cancellationToken);
        }

        public async Task SendPasswordResetEmail(User user, string passwordResetToken, CancellationToken cancellationToken)
        {
            var message = CreatePasswordResetMessage(user, passwordResetToken);

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _settings.SmtpServer,
                _settings.Port,
                MailKit.Security.SecureSocketOptions.StartTls,
                cancellationToken
            );
            await client.AuthenticateAsync(
                _settings.From,
                _settings.Password,
                cancellationToken
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}

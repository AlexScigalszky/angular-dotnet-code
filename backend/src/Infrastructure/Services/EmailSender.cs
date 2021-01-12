using Core.Configuration;
using Core.Interfaces;
using Infrastructure.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IAppLogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailSettings> emailSettings, IAppLogger<EmailSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        /// <summary>
        // Send a email using authentication with the server
        /// </summary>
        /// <param name="from"></param>
        /// <param name="nameTo"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>sucess</returns>
        /// <exception cref="Infrastructure.Exceptions.InfrastructureException">When error happend</exception>
        public async Task<bool> SendEmailAsync(string from, string nameTo, string to, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(from, _emailSettings.Sender));
                mimeMessage.To.Add(new MailboxAddress(nameTo, to));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    _logger.LogInformation("SmtpClient Connected");

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                    _logger.LogInformation("SmtpClient Authenticated");

#if !DEBUG
                    await client.SendAsync(mimeMessage);
                    _logger.LogInformation("Email: " + to);                        
#endif

                    await client.DisconnectAsync(true);
                    _logger.LogInformation("SmtpClient Disconnect");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("NotSendedEmail: " + to, ex);
                throw new InfrastructureException("Email Not Sended");
            }
        }
    }
}

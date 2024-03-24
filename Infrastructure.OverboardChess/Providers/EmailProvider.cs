using Aplication.OverboardChess.Providers;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace Infrastructure.OverboardChess.Providers
{
    public class EmailProvider(IOptions<EmailProviderSettings> settings) : IEmailProvider
    {
        public void Send(Email email)
        {
            string smtpServer = settings.Value.Server;
            int smtpPort = settings.Value.Port;
            string smtpUsername = settings.Value.Username;
            string smtpPassword = settings.Value.Password;
            string from = settings.Value.Email;

            MailMessage message = new(from, email.To)
            {
                Subject = email.Subject,
                Body = email.Body
            };

            using var client = new SmtpClient(smtpServer, smtpPort);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.Send(message);
        }
    }

    public class EmailProviderSettings
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}

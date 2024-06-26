using Demo_Dal.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WeddingGem.API.Helper
{
    public class EmailService : IMailServices
    {
        private readonly MailSettings _options;

        public EmailService(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }
        public void SendEmail(Email email)
        {

            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(email.To));

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = email.Body;

            mail.Body = bodyBuilder.ToMessageBody();
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);

            // Authenticate with the SMTP server using the provided credentials
            smtp.Authenticate(_options.Email, _options.Password);

            // Send the email
            smtp.Send(mail);

            // Disconnect from the SMTP server
            smtp.Disconnect(true);
        }
    }
}

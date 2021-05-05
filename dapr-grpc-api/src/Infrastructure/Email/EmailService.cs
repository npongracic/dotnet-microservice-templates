using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _settings;

        public EmailService(AppSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (bool.Parse(_settings.SendEmail)) {
                var emailMessage = new MailMessage();

                emailMessage.To.Add(new MailAddress(email));
                emailMessage.Subject = subject;
                emailMessage.Body = message;

                await SendEmailAsync(emailMessage);
            }
        }

        public async Task SendEmailAsync(MailMessage emailMessage)
        {
            if (bool.Parse(_settings.SendEmail)) { 
                emailMessage.From = new MailAddress(_settings.Email, _settings.Name);
                emailMessage.IsBodyHtml = true;

                using (var client = new SmtpClient(_settings.SmtpHost, int.Parse(_settings.SmtpPort))) {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_settings.SmtpUsername, _settings.SmtpPassword);
                    client.EnableSsl = false;

                    await client.SendMailAsync(emailMessage).ConfigureAwait(false);
                }
            }
        }
    }
}

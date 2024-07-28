using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Helpers.Models.Email;
using JobPortal.Infrastructure.Configurations;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace JobPortal.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var client = new MailjetClient(_emailSettings.ApiKey, _emailSettings.ApiSecret);

            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "albionerr@gmail.com")
            .Property(Send.FromName, "Job Portal")
            .Property(Send.Subject, emailMessage.Subject)
            .Property(Send.TextPart, emailMessage.Body)
            .Property(Send.Recipients, new JArray
            {
            new JObject
            {
                { "Email", emailMessage.ToEmail }
            }
            });

            await client.PostAsync(request);
        }
    }
}

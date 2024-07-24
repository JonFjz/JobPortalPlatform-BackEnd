using JobPortal.Application.Helpers.Models.Email;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}

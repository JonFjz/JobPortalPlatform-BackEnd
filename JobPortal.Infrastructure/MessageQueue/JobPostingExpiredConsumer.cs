using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Helpers.Models.Email;
using JobPortal.Application.Helpers.Models.Messaging;
using JobPortal.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class JobPostingExpiredConsumer : IConsumer<JobPostingExpiredNotification>
{
    private readonly ILogger<JobPostingExpiredConsumer> _logger;
    private readonly IEmailService _emailService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JobPostingExpiredConsumer(
        ILogger<JobPostingExpiredConsumer> logger,
        IEmailService emailService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _emailService = emailService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(ConsumeContext<JobPostingExpiredNotification> context)
    {
        var jobPostingId = context.Message.JobPostingId;

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var repository = unitOfWork.Repository<JobPosting>();

            var jobPosting = await repository.GetByIdAsync(jp => jp.Id == jobPostingId);

            if (jobPosting == null)
            {
                _logger.LogWarning($"Job Posting with ID {jobPostingId} not found. Email not sent.");
                return;
            }

            var employerEmail = jobPosting.NotificationEmail;
            var jobTitle = jobPosting.Title;

            if (string.IsNullOrEmpty(employerEmail))
            {
                _logger.LogWarning($"No email address found for job posting with ID {jobPostingId}. Email not sent.");
                return;
            }

            _logger.LogInformation($"Job Posting with ID {jobPostingId} has expired. Sending email to {employerEmail}.");

            var emailMessage = new EmailMessage
            {
                ToEmail = employerEmail,
                Subject = "Your Job Posting has Expired",
                Body = $"Your job posting '{jobTitle}' has expired."
            };

            await _emailService.SendEmailAsync(emailMessage);
        }
    }
}

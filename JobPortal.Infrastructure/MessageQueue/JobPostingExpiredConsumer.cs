using JobPortal.Application.Helpers.Models.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace JobPortal.Infrastructure.MessageQueue
{
    public class JobPostingExpiredConsumer : IConsumer<JobPostingExpiredNotification>
    {
        private readonly ILogger<JobPostingExpiredConsumer> _logger;

        public JobPostingExpiredConsumer(ILogger<JobPostingExpiredConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<JobPostingExpiredNotification> context)
        {
            var jobPostingId = context.Message.JobPostingId;
            _logger.LogInformation($"Job Posting with ID {jobPostingId} has expired.");
            await Task.CompletedTask; 
        }
    }
}
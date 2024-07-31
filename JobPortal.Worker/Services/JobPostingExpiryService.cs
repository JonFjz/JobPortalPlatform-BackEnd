using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Helpers.Models.Messaging;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using MassTransit;

namespace JobPortal.Worker.Services
{

    public class JobPostingExpiryService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBusControl _bus;

        public JobPostingExpiryService(IServiceScopeFactory serviceScopeFactory, IBusControl bus)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckExpiredJobPostings();
                await Task.Delay(TimeSpan.FromSeconds(7), stoppingToken);
            }
        }

        private async Task CheckExpiredJobPostings()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var repository = unitOfWork.Repository<JobPosting>();

                var expiredJobPostings = await repository.GetByConditionAsync(jp => jp.PremiumUntil < DateTime.Now && jp.SubscriptionStatus == SubscriptionStatus.Active);

                foreach (var jobPosting in expiredJobPostings)
                {
                    jobPosting.SubscriptionStatus = SubscriptionStatus.Expired;

                    await repository.UpdateAsync(jobPosting);

                    unitOfWork.Complete();

                    await _bus.Publish(new JobPostingExpiredNotification { JobPostingId = jobPosting.Id });
                }
            }
        }
    }

}
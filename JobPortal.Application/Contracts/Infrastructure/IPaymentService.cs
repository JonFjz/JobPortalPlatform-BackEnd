using JobPortal.Domain.Entities;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<JobPosting> UpgradeToPremium(int jobPostingId);
        Task<JobPosting> UpdateJobPostingPaymentSucceeded(string paymentIntentId);
        Task<JobPosting> UpdateJobPostingPaymentFailed(string paymentIntentId);

    }
}

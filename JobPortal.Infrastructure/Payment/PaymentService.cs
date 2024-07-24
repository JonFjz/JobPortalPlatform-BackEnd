using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace JobPortal.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private const decimal DefaultStandardPrice = 100.00m; 

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public async Task<JobPosting> UpgradeToPremium(int jobPostingId)
        {
                var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(jp => jp.Id == jobPostingId);
                if (jobPosting == null) return null;

                if (jobPosting.StandardPrice < DefaultStandardPrice)
                {
                    jobPosting.StandardPrice = DefaultStandardPrice;
                }

                long amountInCents = (long)(jobPosting.StandardPrice * 100);

                var service = new PaymentIntentService();
                PaymentIntent intent;

                if (string.IsNullOrEmpty(jobPosting.PaymentIntentId))
                {
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = amountInCents,
                        Currency = "eur",
                        PaymentMethodTypes = new List<string> { "card" },
                        Metadata = new Dictionary<string, string>
                        {
                            { "JobPostingId", jobPostingId.ToString() }
                        }
                    };
                    intent = await service.CreateAsync(options);
                    jobPosting.PaymentIntentId = intent.Id;
                    jobPosting.ClientSecret = intent.ClientSecret;
                }
                else
                {
                    var options = new PaymentIntentUpdateOptions
                    {
                        Amount = amountInCents,
                        Metadata = new Dictionary<string, string>
                        {
                            { "JobPostingId", jobPostingId.ToString() } 
                        }
                    };
                    await service.UpdateAsync(jobPosting.PaymentIntentId, options);
                }

                jobPosting.UpgradedAt = DateTime.Now;
                jobPosting.PremiumUntil = DateTime.Now.AddMonths(1); // 1 month premium period

                await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
                _unitOfWork.Complete();

                return jobPosting;
            }
           
        

        public async Task<JobPosting> UpdateJobPostingPaymentSucceeded(string paymentIntentId)
        {
                var intentId = await _unitOfWork.Repository<JobPosting>().GetByConditionAsync(jp => jp.PaymentIntentId == paymentIntentId);
                if (intentId == null) return null;

                var jobPosting = intentId.FirstOrDefault();
                if (jobPosting == null) return null;
            
            jobPosting.Status = PaymentStatus.Completed;

            await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
                _unitOfWork.Complete();

                return jobPosting;
        }
          

        public async Task<JobPosting> UpdateJobPostingPaymentFailed(string paymentIntentId)
        {

                var intentId = await _unitOfWork.Repository<JobPosting>().GetByConditionAsync(jp => jp.PaymentIntentId == paymentIntentId);
                if (intentId == null) return null;

                var jobPosting = intentId.FirstOrDefault();
                if (jobPosting == null) return null;
            
            jobPosting.Status = PaymentStatus.Failed;

            await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
                _unitOfWork.Complete();

                return jobPosting;
        }     
    }
}
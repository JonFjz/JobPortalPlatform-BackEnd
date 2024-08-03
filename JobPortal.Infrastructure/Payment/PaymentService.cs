using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private const decimal DefaultStandardPrice = 100.00m;

    public PaymentService(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config ?? throw new ArgumentNullException(nameof(config));

        var stripeSecretKey = _config["Stripe:SecretKey"];
        if (string.IsNullOrEmpty(stripeSecretKey))
        {
            throw new InvalidOperationException("Stripe SecretKey is not configured.");
        }
        StripeConfiguration.ApiKey = stripeSecretKey;
    }


    public async Task<JobPosting> UpgradeToPremium(int jobPostingId)
    {
        var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(jp => jp.Id == jobPostingId);
        if (jobPosting == null) return null;

        long amountInCents = (long)(DefaultStandardPrice * 100);

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

        jobPosting.PremiumUntil = DateTime.Now.AddMonths(1); 

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


        jobPosting.UpgradedAt = DateTime.Now;
        jobPosting.PaymentStatus = PaymentStatus.Completed;
        jobPosting.SubscriptionStatus = SubscriptionStatus.Active;

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
        
        jobPosting.PaymentStatus = PaymentStatus.Failed;

        await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
        _unitOfWork.Complete();

        return jobPosting;
    }
}
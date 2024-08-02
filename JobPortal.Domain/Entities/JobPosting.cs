using System.Text.Json.Serialization;
using JobPortal.Domain.Enums;

namespace JobPortal.Domain.Entities
{
    public class JobPosting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public DateTime ClosingDate { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredSkills { get; set; }
        public WorkType WorkType { get; set; } 
        public WorkLevel WorkLevel { get; set; }
        public string NotificationEmail { get; set; } 

        //premium job posting related fields
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Pending;
        public DateTime? UpgradedAt { get; set; }
        public DateTime? PremiumUntil { get; set; }
        public decimal StandardPrice { get; set; }

        //stripe payment-related fields
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }


        public int EmployerId { get; set; }
        [JsonIgnore]
        public Employer Employer { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    }
}

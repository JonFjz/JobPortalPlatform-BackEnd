﻿using JobPortal.Domain.Enums;

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
        public WorkType WorkType { get; set; } 
        public WorkLevel WorkLevel { get; set; }
        public string NotificationEmail { get; set; } //email to send updates when a job seeker applies

        //premium job posting related fields
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime? UpgradedAt { get; set; }
        public DateTime? PremiumUntil { get; set; }
        public decimal StandardPrice { get; set; }

        // Stripe payment-related fields
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }


        public int EmployerId { get; set; }
        public Employer Employer { get; set; }



    }
}

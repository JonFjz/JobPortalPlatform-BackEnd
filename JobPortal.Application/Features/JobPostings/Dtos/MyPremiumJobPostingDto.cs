using JobPortal.Domain.Enums;

namespace JobPortal.Application.Features.JobPostings.Dtos;

public class MyPremiumJobPostingDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DatePosted { get; set; } 
    public DateTime ClosingDate { get; set; }
    public string Responsibilities { get; set; }
    public WorkType WorkType { get; set; } 
    public WorkLevel WorkLevel { get; set; }
    public string NotificationEmail { get; set; } 

    //premium job posting related fields
    public PaymentStatus PaymentStatus { get; set; } 
    public SubscriptionStatus SubscriptionStatus { get; set; }
    public DateTime? UpgradedAt { get; set; }
    public DateTime? PremiumUntil { get; set; }
}
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
        public WorkType WorkType { get; set; } 
        public WorkLevel WorkLevel { get; set; }
        public string NotificationEmail { get; set; } //email to send updates when a job seeker applies
        public bool IsPremium { get; set; } = false;


        public int EmployerId { get; set; }
        public Employer Employer { get; set; }

    }
}

using JobPortal.Domain.Enums;

namespace JobPortal.Application.Helpers.Models.Messaging
{
    public class JobPostingUpgradedNotification
    {
        public int JobPostingId { get; set; }
        public PaymentStatus Status { get; set; }
    }

}

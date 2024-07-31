using JobPortal.Domain.Enums;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting
{
    public class CreateJobPostingCommand : IRequest<int>
    {
        public string Title { get; set; }
        public DateTime ClosingDate { get; set; }
        public WorkType WorkType { get; set; }
        public WorkLevel WorkLevel { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredSkills { get; set; }
        public string NotificationEmail { get; set; }

    }
}

using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting
{
    public class UpdateJobPostingCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredSkills { get; set; }
        public string WorkType { get; set; }
        public string WorkLevel { get; set; }
        public string NotificationEmail { get; set; }
    }
}

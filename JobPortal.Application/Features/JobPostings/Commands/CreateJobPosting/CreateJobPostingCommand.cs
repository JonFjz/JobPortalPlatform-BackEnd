using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting
{
    public class CreateJobPostingCommand : IRequest<int>
    {
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
    }
}

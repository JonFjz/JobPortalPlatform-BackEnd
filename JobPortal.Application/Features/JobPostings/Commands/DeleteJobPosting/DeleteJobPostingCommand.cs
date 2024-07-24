using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.DeleteJobPosting
{
    public class DeleteJobPostingCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteJobPostingCommand(int id)
        {
            Id = id;
        }
    }
}
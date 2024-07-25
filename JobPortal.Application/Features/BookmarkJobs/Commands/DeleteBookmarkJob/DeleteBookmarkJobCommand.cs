using MediatR;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.RemoveBookmarkJob
{
    public class DeleteBookmarkJobCommand: IRequest<bool>
    {
        public int JobPostingId { get; set; }

        public DeleteBookmarkJobCommand(int jobPostingId)
        {
            JobPostingId = jobPostingId;
        }
    }
}

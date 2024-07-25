using MediatR;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.CreateBookmarkJob
{
    public class CreateBookmarkJobCommand : IRequest<int>
    {
        public int JobPostingId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

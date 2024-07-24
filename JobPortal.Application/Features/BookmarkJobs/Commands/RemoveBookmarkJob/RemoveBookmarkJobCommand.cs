using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.RemoveBookmarkJob
{
    public class RemoveBookmarkJobCommand: IRequest<bool>
    {
        public int JobPostingId { get; set; }

        public RemoveBookmarkJobCommand(int jobPostingId)
        {
            JobPostingId = jobPostingId;
        }
    }
}

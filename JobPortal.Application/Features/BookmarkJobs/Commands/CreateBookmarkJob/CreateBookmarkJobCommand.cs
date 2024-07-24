using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.CreateBookmarkJob
{
    public class CreateBookmarkJobCommand : IRequest<int>
    {
        public int JobPostingId { get; set; }
    }
}

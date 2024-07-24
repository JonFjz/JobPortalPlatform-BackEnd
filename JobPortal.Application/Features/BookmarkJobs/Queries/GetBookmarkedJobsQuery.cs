using JobPortal.Application.Features.BookmarkJobs.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.BookmarkJobs.Queries
{
    public class GetBookmarkedJobsQuery: IRequest<List<BookmarkedJobDto>>
    {
    }
}

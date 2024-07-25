using JobPortal.Application.Features.BookmarkJobs.Dtos;
using MediatR;

namespace JobPortal.Application.Features.BookmarkJobs.Queries
{
    public class GetBookmarkedJobsQuery: IRequest<List<BookmarkedJobDto>>
    {
    }
}

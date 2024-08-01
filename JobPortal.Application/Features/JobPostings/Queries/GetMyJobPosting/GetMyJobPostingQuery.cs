using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetMyJobPosting;

public class GetMyJobPostingQuery : IRequest<PagedResult<MyJobPostingDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetMyJobPostingQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
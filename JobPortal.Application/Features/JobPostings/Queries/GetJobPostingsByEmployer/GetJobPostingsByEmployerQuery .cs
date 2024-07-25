using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings
{
    public class GetJobPostingsByEmployerQuery : IRequest<PagedResult<JobPostingOverviewDto>>
    {
        public int EmployerId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetJobPostingsByEmployerQuery(int employerId, int pageNumber, int pageSize)
        {
            EmployerId = employerId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
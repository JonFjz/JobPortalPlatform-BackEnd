using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Queries.GetJobApplicationsForJobPosting
{
    public class GetJobApplicationsQuery : IRequest<PagedResult<JobApplicationDto>>
    {
        public int JobPostingId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetJobApplicationsQuery(int jobPostingId, int pageNumber, int pageSize)
        {
            JobPostingId = jobPostingId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
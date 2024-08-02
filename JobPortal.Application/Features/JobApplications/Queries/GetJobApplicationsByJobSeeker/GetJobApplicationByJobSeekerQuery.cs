using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Queries.GetJobPostingByJobSeeker
{
    public class GetJobApplicationByJobSeekerQuery : IRequest<PagedResult<JobApplicatinForJobSeekerDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetJobApplicationByJobSeekerQuery(int pageNumber, int pageSize)
        {            
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

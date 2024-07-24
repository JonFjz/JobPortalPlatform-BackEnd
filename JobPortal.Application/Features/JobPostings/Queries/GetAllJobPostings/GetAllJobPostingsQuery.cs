using JobPortal.Application.Features.JobPostings.Dtos;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings
{
    public class GetAllJobPostingsQuery : IRequest<List<JobPostingDto>>
    {


    }
}

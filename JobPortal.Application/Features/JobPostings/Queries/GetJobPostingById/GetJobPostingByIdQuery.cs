using JobPortal.Application.Features.JobPostings.Dtos;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetJobPostingById
{
    public class GetJobPostingByIdQuery : IRequest<JobPostingDto>
    {
        public int Id { get; set; }

        public GetJobPostingByIdQuery(int id)
        {
            Id = id;
        }
    }
}


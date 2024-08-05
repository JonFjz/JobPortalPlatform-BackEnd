using JobPortal.Application.Features.JobPostings.Dtos;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetRecommendJob;

public class GetRecommendJobQuery:IRequest<List<JobPostingDto>>
{
    
}
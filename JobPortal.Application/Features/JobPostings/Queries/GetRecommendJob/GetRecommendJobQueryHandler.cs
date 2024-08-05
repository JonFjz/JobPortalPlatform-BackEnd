using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.JobPostings.Queries.GetRecommendJob;

public class GetRecommendJobQueryHandler:IRequestHandler<GetRecommendJobQuery,List<JobPostingDto>>
{
    private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
    private readonly ISearchService _searchService;
    private readonly IUnitOfWork _unitOfWork;
    public GetRecommendJobQueryHandler(IClaimsPrincipalAccessor claimsPrincipalAccessor,ISearchService searchService,IUnitOfWork unitOfWork)
    {
        _claimsPrincipalAccessor = claimsPrincipalAccessor;
        _searchService = searchService;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<JobPostingDto>> Handle(GetRecommendJobQuery request, CancellationToken cancellationToken)
    {
        var jobSeekerFromToken = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

        var jobSeeker = await _unitOfWork.Repository<Domain.Entities.JobSeeker>()
            .GetByCondition(x => x.Id == jobSeekerFromToken.Id)
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .Include(x => x.Skills)
            .FirstOrDefaultAsync();

        var jobPostingDto = _searchService.RecommendJobsAsync(jobSeeker);
        return jobPostingDto.Result;
    }
}
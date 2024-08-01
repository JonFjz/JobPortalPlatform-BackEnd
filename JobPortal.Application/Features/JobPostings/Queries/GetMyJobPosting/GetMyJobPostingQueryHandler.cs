using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.JobPostings.Queries.GetMyJobPosting;

public class GetMyJobPostingQueryHandler:IRequestHandler<GetMyJobPostingQuery,PagedResult<MyJobPostingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

    public GetMyJobPostingQueryHandler(IUnitOfWork unitOfWork,IMapper mapper,IClaimsPrincipalAccessor claimsPrincipalAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimsPrincipalAccessor = claimsPrincipalAccessor;
    }
    public async Task<PagedResult<MyJobPostingDto>> Handle(GetMyJobPostingQuery request, CancellationToken cancellationToken)
    {
        var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();
        if (request.PageNumber <= 0) request.PageNumber = 1;
        if (request.PageSize <= 0) request.PageSize = 10;

        var totalCount = await _unitOfWork.Repository<JobPosting>()
            .CountByConditionAsync(jp => jp.EmployerId == employer.Id&& jp.PaymentStatus!=PaymentStatus.Completed);
        
        var jobPostings = await _unitOfWork.Repository<JobPosting>()
            .GetByCondition(jp => jp.EmployerId == employer.Id && jp.PaymentStatus!=PaymentStatus.Completed)
            .OrderByDescending(jp => jp.DatePosted)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        
        var jobPostingDtos = _mapper.Map<List<MyJobPostingDto>>(jobPostings);

        return new PagedResult<MyJobPostingDto>
        {
            Items = jobPostingDtos,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.JobPostings.Queries.GetJobPostingsByEmployerId
{
    public class GetJobPostingsByEmployerIdQueryHandler : IRequestHandler<GetJobPostingsByEmployerIdQuery, PagedResult<JobPostingOverviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobPostingsByEmployerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobPostingOverviewDto>> Handle(GetJobPostingsByEmployerIdQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var totalCount = await _unitOfWork.Repository<JobPosting>()
                .CountByConditionAsync(jp => jp.EmployerId == request.EmployerId);

            var jobPostings = await _unitOfWork.Repository<JobPosting>()
                .GetByCondition(jp => jp.EmployerId == request.EmployerId && jp.ClosingDate > DateTime.Now)
                .OrderByDescending(jp => jp.SubscriptionStatus == SubscriptionStatus.Active)
                .ThenByDescending(jp => jp.DatePosted)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var jobPostingDtos = _mapper.Map<List<JobPostingOverviewDto>>(jobPostings);

            return new PagedResult<JobPostingOverviewDto>
            {
                Items = jobPostingDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

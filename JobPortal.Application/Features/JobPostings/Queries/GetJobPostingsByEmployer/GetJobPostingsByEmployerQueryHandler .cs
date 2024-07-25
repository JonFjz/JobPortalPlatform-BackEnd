using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings;
using JobPortal.Application.Helpers.Models.Pagination;

namespace JobPortal.Application.Features.JobPostings.Queries
{
    public class GetJobPostingsByEmployerQueryHandler : IRequestHandler<GetJobPostingsByEmployerQuery, PagedResult<JobPostingOverviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobPostingsByEmployerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobPostingOverviewDto>> Handle(GetJobPostingsByEmployerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var totalCount = await _unitOfWork.Repository<JobPosting>()
                .CountByConditionAsync(jp => jp.EmployerId == request.EmployerId);

            var jobPostings = await _unitOfWork.Repository<JobPosting>()
                .GetPagedByConditionAsync(
                    jp => jp.EmployerId == request.EmployerId,
                    (request.PageNumber - 1) * request.PageSize,
                    request.PageSize);

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

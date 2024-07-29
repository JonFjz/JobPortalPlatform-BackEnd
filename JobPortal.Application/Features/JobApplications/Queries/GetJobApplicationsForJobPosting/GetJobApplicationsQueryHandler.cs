using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Queries.GetJobApplicationsForJobPosting
{
    public class GetJobApplicationsQueryHandler : IRequestHandler<GetJobApplicationsQuery, PagedResult<JobApplicationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;

        public GetJobApplicationsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IClaimsPrincipalAccessor claimsAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsAccessor = claimsAccessor;
        }

        public async Task<PagedResult<JobApplicationDto>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var employer = await _claimsAccessor.GetCurrentEmployerAsync();
            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.JobPostingId);

            if (jobPosting == null || jobPosting.EmployerId != employer.Id)
            {
                throw new KeyNotFoundException("Job posting not found or you do not have permission to access this job posting.");
            }

            var totalCount = await _unitOfWork.Repository<JobApplication>()
                .CountByConditionAsync(ja => ja.JobPostingId == request.JobPostingId);

            var jobApplications = await _unitOfWork.Repository<JobApplication>()
                .GetPagedByConditionAsync(
                    ja => ja.JobPostingId == request.JobPostingId,
                    (request.PageNumber - 1) * request.PageSize,
                    request.PageSize);

            var jobApplicationDtos = _mapper.Map<List<JobApplicationDto>>(jobApplications);

            return new PagedResult<JobApplicationDto>
            {
                Items = jobApplicationDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

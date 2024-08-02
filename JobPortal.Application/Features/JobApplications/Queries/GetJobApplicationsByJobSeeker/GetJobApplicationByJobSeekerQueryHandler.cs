using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.JobApplications.Queries.GetJobPostingByJobSeeker
{
    public class GetJobApplicationByJobSeekerQueryHandler : IRequestHandler<GetJobApplicationByJobSeekerQuery, PagedResult<JobApplicatinForJobSeekerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetJobApplicationByJobSeekerQueryHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobApplicatinForJobSeekerDto>> Handle(GetJobApplicationByJobSeekerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var totalCount = await _unitOfWork.Repository<JobApplication>()
                .CountByConditionAsync(x => x.JobSeekerId == jobSeeker.Id);

            var jobApplications = await _unitOfWork.Repository<JobApplication>()
                .GetByCondition(x => x.JobSeekerId == jobSeeker.Id)
                .Include(x => x.JobPosting)
                .ThenInclude(jp => jp.Employer)
                .OrderByDescending(x =>x.AppliedOn)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var jobApplicaitnsToReturn = _mapper.Map<List<JobApplicatinForJobSeekerDto>>(jobApplications);
            return new PagedResult<JobApplicatinForJobSeekerDto>
            {
                Items = jobApplicaitnsToReturn,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

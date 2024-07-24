using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetJobPostingById
{
    internal class GetJobPostingByIdQueryHandler : IRequestHandler<GetJobPostingByIdQuery, JobPostingDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobPostingByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<JobPostingDto> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
        {
            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.Id);

            if (jobPosting == null)
            {
                throw new KeyNotFoundException("Job Posting not found.");
            }

            return _mapper.Map<JobPostingDto>(jobPosting);
        }
    }
}

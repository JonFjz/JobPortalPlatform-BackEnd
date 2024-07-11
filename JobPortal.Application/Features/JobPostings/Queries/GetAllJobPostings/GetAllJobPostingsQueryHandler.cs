using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Contracts.Persistence.Job;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings
{
    public class GetAllJobPostingsQueryHandler : IRequestHandler<GetAllJobPostingsQuery, List<JobPostingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllJobPostingsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<JobPostingDto>> Handle(GetAllJobPostingsQuery request, CancellationToken cancellationToken)
        {
            var jobPostings = await _unitOfWork.Repository<JobPosting>().GetAllAsync();
            var data = _mapper.Map<List<JobPostingDto>>(jobPostings);

            return data;
        }
    }
}

using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Queries.GetAllJobPostings
{
    public class GetAllJobPostingsQueryHandler : IRequestHandler<GetAllJobPostingsQuery, List<JobPostingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAllJobPostingsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,ICacheService cacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<List<JobPostingDto>> Handle(GetAllJobPostingsQuery request, CancellationToken cancellationToken)
        {
            if (_cacheService.GetCacheAsync < List<JobPostingDto> >("1")== null)
            {

            }
            var jobPostings = await _unitOfWork.Repository<JobPosting>().GetAllAsync();
            var data = _mapper.Map<List<JobPostingDto>>(jobPostings);

            return data;
        }
    }
}

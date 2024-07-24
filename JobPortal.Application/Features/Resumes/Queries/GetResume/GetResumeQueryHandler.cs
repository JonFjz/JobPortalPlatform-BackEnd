using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Queries.GetResume
{
    public class GetResumeHandler : IRequestHandler<GetResumeQuery, ResumeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;
        private readonly IMapper _mapper;

        public GetResumeHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
            _mapper = mapper;
        }

        public async Task<ResumeDto> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsAccessor.GetCurrentJobSeekerAsync();
            
            var resume = (await _unitOfWork.Repository<Resume>().GetByConditionAsync(r => r.JobSeekerId == jobSeeker.Id)).FirstOrDefault();

            if (resume == null)
            {
                return null;
            }

            var resumeDto = _mapper.Map<ResumeDto>(resume);
            return resumeDto;
        }

    }
}

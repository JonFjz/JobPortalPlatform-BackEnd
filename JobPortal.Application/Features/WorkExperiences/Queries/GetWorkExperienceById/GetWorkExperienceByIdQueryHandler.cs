using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.WorkExperiences.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Queries.GetWorkExperienceById
{
    public class GetWorkExperienceByIdQueryHandler : IRequestHandler<GetWorkExperienceByIdQuery, WorkExperienceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetWorkExperienceByIdQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }
        public async Task<WorkExperienceDto> Handle(GetWorkExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var workExperiences = await _unitOfWork.Repository<WorkExperience>().GetByIdAsync(e => e.Id == request.Id);

            if (workExperiences == null || workExperiences.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Work Experience not found.");
            }

            return _mapper.Map<WorkExperienceDto>(workExperiences);
        }
    }
}

using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.WorkExperiences.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Queries.GetAllWorkExperiences
{
    public class GetAllWorkExperiencesQueryHandler : IRequestHandler<GetAllWorkExperiencesQuery, List<WorkExperienceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetAllWorkExperiencesQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<List<WorkExperienceDto>> Handle(GetAllWorkExperiencesQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var workExperiences = await _unitOfWork.Repository<WorkExperience>()
                .GetByConditionAsync(e => e.JobSeekerId == jobSeeker.Id);

            return _mapper.Map<List<WorkExperienceDto>>(workExperiences);
        }

       
    }
}

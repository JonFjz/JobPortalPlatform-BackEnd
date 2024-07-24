using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience
{
    public class CreateWorkExperienceCommandHandler : IRequestHandler<CreateWorkExperienceCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public CreateWorkExperienceCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var workExperienceToCreate = _mapper.Map<WorkExperience>(request);
            workExperienceToCreate.JobSeekerId = jobSeeker.Id;  

            await _unitOfWork.Repository<WorkExperience>().CreateAsync(workExperienceToCreate);
            _unitOfWork.Complete();

            return workExperienceToCreate.Id;
        }
    }
}

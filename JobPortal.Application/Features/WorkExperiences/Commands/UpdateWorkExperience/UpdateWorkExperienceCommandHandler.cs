using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommandHandler : IRequestHandler<UpdateWorkExperienceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public UpdateWorkExperienceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper ,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var workExperienceToUpdate = await _unitOfWork.Repository<WorkExperience>().GetByIdAsync(e => e.Id == request.Id);

            if (workExperienceToUpdate == null || workExperienceToUpdate.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Work Experience not found.");
            }

            _mapper.Map(request, workExperienceToUpdate);

            await _unitOfWork.Repository<WorkExperience>().UpdateAsync(workExperienceToUpdate);
            _unitOfWork.Complete();

        }
    }
}
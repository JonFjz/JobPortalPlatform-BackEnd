using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.DeleteWorkExperience
{
    public class DeleteWorkExperienceCommandHandler : IRequestHandler<DeleteWorkExperienceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteWorkExperienceCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var workExperience = await _unitOfWork.Repository<WorkExperience>().GetByIdAsync(e => e.Id == request.Id);

            if (workExperience == null || workExperience.JobSeekerId != jobSeeker.Id)
                throw new KeyNotFoundException("Work Experience not found.");

            _unitOfWork.Repository<WorkExperience>().Delete(workExperience);
            _unitOfWork.Complete();
        }
    }
}

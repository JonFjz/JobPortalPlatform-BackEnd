using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteSkillCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(e => e.Id == request.Id);

            if (skill == null || skill.JobSeekerId != jobSeeker.Id)
                throw new KeyNotFoundException("Skill not found.");

            _unitOfWork.Repository<Skill>().Delete(skill);
            _unitOfWork.Complete();
        }
    }
}

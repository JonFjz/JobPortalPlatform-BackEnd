using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public UpdateSkillCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(e => e.Id == request.Id);

            if (skill == null || skill.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Education not found.");
            }

            _mapper.Map(request, skill);

            await _unitOfWork.Repository<Skill>().UpdateAsync(skill);
            _unitOfWork.Complete();

        }
    }
}
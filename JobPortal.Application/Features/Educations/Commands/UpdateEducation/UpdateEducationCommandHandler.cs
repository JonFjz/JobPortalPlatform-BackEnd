using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Educations.Commands.UpdateEducation
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public UpdateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper ,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var education = await _unitOfWork.Repository<Education>().GetByIdAsync(e => e.Id == request.Id);

            if (education == null || education.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Education not found.");
            }

            _mapper.Map(request, education);

            await _unitOfWork.Repository<Education>().UpdateAsync(education);
            _unitOfWork.Complete();

        }
    }
}
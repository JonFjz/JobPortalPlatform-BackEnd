using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Educations.Commands.DeleteEducation
{
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteEducationCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var education = await _unitOfWork.Repository<Education>().GetByIdAsync(e => e.Id == request.Id);

            if (education == null || education.JobSeekerId != jobSeeker.Id)
                throw new KeyNotFoundException("Education not found.");

            _unitOfWork.Repository<Education>().Delete(education);
            _unitOfWork.Complete();
        }
    }
}

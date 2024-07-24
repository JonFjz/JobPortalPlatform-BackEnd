using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Commands.DeleteResume
{
    public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;
        private readonly IBlobStorageService _blobService;

        public DeleteResumeCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor, IBlobStorageService blobService)
        {
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
            _blobService = blobService;
        }

        public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsAccessor.GetCurrentJobSeekerAsync();
            var resume = await _unitOfWork.Repository<Resume>().GetByConditionAsync(r => r.JobSeekerId == jobSeeker.Id);

            if (resume == null)
                throw new KeyNotFoundException("Resume not found.");

            await _blobService.DeleteFileAsync(resume.First().Url);

            _unitOfWork.Repository<Resume>().Delete(resume.First());
            _unitOfWork.Complete();

        }
    }
}

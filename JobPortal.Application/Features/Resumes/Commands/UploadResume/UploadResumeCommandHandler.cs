using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Commands.UploadResume
{
    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, int>
    {
        private readonly IBlobStorageService _blobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;

        public UploadResumeCommandHandler(IBlobStorageService blobService, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor)
        {
            _blobService = blobService;
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
        }

        public async Task<int> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsAccessor.GetCurrentJobSeekerAsync();

            var uniqueBlobName = await _blobService.UploadResumeAsync(request.File.OpenReadStream(), request.File.FileName);

            var resume = new Resume
            {
                OriginalResumeName = request.File.FileName,
                JobSeekerId = jobSeeker.Id,
                BlobUniqueName = uniqueBlobName
            };

            await _unitOfWork.Repository<Resume>().CreateAsync(resume);
            _unitOfWork.Complete();

            return resume.Id;
        }
    }
}
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Queries.DownloadResume
{
    public class DownloadResumeHandler : IRequestHandler<DownloadResumeQuery, DownloadResumeResult>
    {
        private readonly IBlobStorageService _blobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;

        public DownloadResumeHandler(IBlobStorageService blobService, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor)
        {
            _blobService = blobService;
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
        }

        public async Task<DownloadResumeResult> Handle(DownloadResumeQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsAccessor.GetCurrentJobSeekerAsync();
            var resume = (await _unitOfWork.Repository<Resume>().GetByConditionAsync(r => r.JobSeekerId == jobSeeker.Id)).FirstOrDefault();

            if (resume == null)
            {
                throw new KeyNotFoundException("Resume not found.");
            }

            var fileData = await _blobService.DownloadResumeAsync(resume.BlobUniqueName);

            return new DownloadResumeResult
            {
                FileData = fileData,
                FileName = resume.OriginalResumeName ?? "resume.pdf"
            };
        }
    }
}
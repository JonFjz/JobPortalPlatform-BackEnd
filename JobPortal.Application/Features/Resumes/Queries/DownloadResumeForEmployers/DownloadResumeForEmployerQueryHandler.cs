using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Resumes.Queries.DownloadResumeForEmployers
{
    public class DownloadResumeForEmployerQueryHandler : IRequestHandler<DownloadResumeForEmployerQuery, DownloadResumeResult>
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly IUnitOfWork _unitOfWork;
        public readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DownloadResumeForEmployerQueryHandler(IBlobStorageService blobStorageService, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _blobStorageService = blobStorageService;
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task<DownloadResumeResult> Handle(DownloadResumeForEmployerQuery request, CancellationToken cancellationToken)
        {
            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();
            if (employer == null)
            {
                throw new UnauthorizedAccessException("Employer not found.");
            }

            var jobApplication = await _unitOfWork.Repository<JobApplication>().GetByIdAsync(e => e.Id == request.JobApplicationId);
            if (jobApplication == null)
            {
                throw new KeyNotFoundException("Job application not found.");
            }

            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == jobApplication.JobPostingId);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException("Job posting not found.");
            }


            if (jobPosting.EmployerId != employer.Id)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resume.");
            }

            var resumeBlobName = jobApplication.SnapshotResumeBlobName;
            if (string.IsNullOrEmpty(resumeBlobName))
            {
                throw new KeyNotFoundException("Resume snapshot not found.");
            }

            var fileData = await _blobStorageService.DownloadResumeAsync(resumeBlobName);

            return new DownloadResumeResult
            {
                FileData = fileData,
                FileName = jobApplication.ResumeOriginalName ?? "resume.pdf"
            };
        }
    }
}

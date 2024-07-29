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

        public DownloadResumeForEmployerQueryHandler(IBlobStorageService blobStorageService, IUnitOfWork unitOfWork)
        {
            _blobStorageService = blobStorageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DownloadResumeResult> Handle(DownloadResumeForEmployerQuery request, CancellationToken cancellationToken)
        {
            var jobApplication = await _unitOfWork.Repository<JobApplication>().GetByIdAsync(e => e.Id == request.JobApplicationId);
            if (jobApplication == null)
            {
                throw new KeyNotFoundException("Job application not found.");
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

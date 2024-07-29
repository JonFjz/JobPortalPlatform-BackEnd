using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Command.ApplyToJob
{
    public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, int>
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;
        private readonly IMapper _mapper;

        public ApplyToJobCommandHandler(
            IBlobStorageService blobStorageService,
            IUnitOfWork unitOfWork,
            IClaimsPrincipalAccessor claimsAccessor,
            IMapper mapper)
        {
            _blobStorageService = blobStorageService;
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
            _mapper = mapper;
        }

        public async Task<int> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsAccessor.GetCurrentJobSeekerAsync();
            var existingApplication = await _unitOfWork.Repository<JobApplication>()
                .GetByConditionAsync(a => a.JobSeekerId == jobSeeker.Id && a.JobPostingId == request.JobPostingId);

            if (existingApplication.Any()) throw new InvalidOperationException("You have already applied for this job.");


            var jobPosting = await _unitOfWork.Repository<JobPosting>()
            .GetByIdAsync(e => e.Id == request.JobPostingId);

            if (jobPosting == null)
            {
                throw new InvalidOperationException("Job posting not found.");
            }

            if (jobPosting.ClosingDate < DateTime.Now)
            {
                throw new InvalidOperationException("This job posting has expired.");
            }

            Resume selectedResume = await HandleResumeAsync(request, jobSeeker.Id);

            var snapshotBlobName = await CreateSnapshotAsync(selectedResume);

            var jobApplication = _mapper.Map<JobApplication>(request);
            jobApplication.JobSeekerId = jobSeeker.Id;
            jobApplication.SnapshotResumeBlobName = snapshotBlobName;
            jobApplication.ResumeOriginalName = selectedResume.OriginalResumeName;

            await _unitOfWork.Repository<JobApplication>().CreateAsync(jobApplication);
            _unitOfWork.Complete();

            return jobApplication.Id;
        }

        private async Task<Resume> HandleResumeAsync(ApplyToJobCommand request, int jobSeekerId)
        {
            Resume selectedResume = null;

            if (request.NewResumeFile != null)
            {
                var existingResumes = await _unitOfWork.Repository<Resume>()
                    .GetByConditionAsync(r => r.JobSeekerId == jobSeekerId);

                if (existingResumes.Any())
                {
                    var existingResume = existingResumes.First();
                    await _blobStorageService.DeleteResumeAsync(existingResume.BlobUniqueName);
                    _unitOfWork.Repository<Resume>().Delete(existingResume);
                    _unitOfWork.Complete();
                }

                var uniqueBlobName = await _blobStorageService.UploadResumeAsync(
                    request.NewResumeFile.OpenReadStream(),
                    request.NewResumeFile.FileName);

                selectedResume = new Resume
                {
                    OriginalResumeName = request.NewResumeFile.FileName,
                    JobSeekerId = jobSeekerId,
                    BlobUniqueName = uniqueBlobName
                };

                await _unitOfWork.Repository<Resume>().CreateAsync(selectedResume);
                _unitOfWork.Complete();
            }
            else if (request.ResumeId.HasValue)
            {
                selectedResume = await _unitOfWork.Repository<Resume>().GetByIdAsync(e => e.Id == request.ResumeId.Value);

                if (selectedResume == null || selectedResume.JobSeekerId != jobSeekerId)
                {
                    throw new InvalidOperationException("Selected resume is invalid or does not belong to the job seeker.");
                }
            }
            else
            {
                throw new InvalidOperationException("No resume selected for application.");
            }

            return selectedResume;
        }

        private async Task<string> CreateSnapshotAsync(Resume resume)
        {
            var resumeData = await _blobStorageService.DownloadResumeAsync(resume.BlobUniqueName);
            using (var resumeStream = new MemoryStream(resumeData))
            {
                return await _blobStorageService.UploadResumeAsync(resumeStream, resume.OriginalResumeName);
            }
        }
    }
}

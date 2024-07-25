using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.CreateBookmarkJob
{
    public class CreateBookmarkJobCommandHandler :IRequestHandler<CreateBookmarkJobCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        public CreateBookmarkJobCommandHandler(IUnitOfWork unitOfWork,IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task<int> Handle(CreateBookmarkJobCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(c => c.Id == request.JobPostingId);
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            if (jobPosting == null)
            {
                throw new Exception("Job not foud");
            }
            var existBookmarJob= _unitOfWork.Repository<BookmarkJob>().GetByCondition(x=>x.JobSeekerId==jobSeeker.Id&&x.JobPostingId==jobPosting.Id).FirstOrDefault();
            if (existBookmarJob!=null)
            {
                throw new Exception("Job is already bookmarked");
            }
            var bookmarkJob = new BookmarkJob
            {
                JobPostingId = request.JobPostingId,
                JobSeekerId = jobSeeker.Id
            };

            await _unitOfWork.Repository<BookmarkJob>().CreateAsync(bookmarkJob);
            return bookmarkJob.JobPostingId;
        }
    }
}

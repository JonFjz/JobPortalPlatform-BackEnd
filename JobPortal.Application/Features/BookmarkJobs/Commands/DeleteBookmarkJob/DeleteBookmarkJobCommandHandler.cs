using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.BookmarkJobs.Commands.RemoveBookmarkJob
{
    public class DeleteBookmarkJobCommandHandler : IRequestHandler<DeleteBookmarkJobCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteBookmarkJobCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task<bool> Handle(DeleteBookmarkJobCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var bookmarkJob = await _unitOfWork.Repository<BookmarkJob>().GetByCondition(x => x.JobSeekerId == jobSeeker.Id && x.JobPostingId == request.JobPostingId).FirstOrDefaultAsync();
            if (bookmarkJob == null)
            {
                throw new KeyNotFoundException("Bookmark not found");
            }
            _unitOfWork.Repository<BookmarkJob>().Delete(bookmarkJob);
            _unitOfWork.Complete();
            return true;
        }
    }
}

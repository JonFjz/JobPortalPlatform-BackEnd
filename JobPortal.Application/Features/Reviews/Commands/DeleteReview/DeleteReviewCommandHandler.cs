using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;            
        }
        public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker= await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(x => x.Id == request.Id);

            if (review == null || review.JobSeekerId != jobSeeker.Id)
                throw new KeyNotFoundException("Review not foud or you are not authorized!");
            _unitOfWork.Repository<Review>().Delete(review);
            _unitOfWork.Complete();
            return true;
        }
    }
}

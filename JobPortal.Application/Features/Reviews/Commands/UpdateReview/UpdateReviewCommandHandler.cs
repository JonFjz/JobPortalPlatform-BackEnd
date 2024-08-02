using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public UpdateReviewCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }
        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var review=await _unitOfWork.Repository<Review>().GetByIdAsync(x=>x.Id==request.Id);
            if(review == null|| review.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Review not foud or you are not authorized");
            }
            review.Content=request.Content;
            await _unitOfWork.Repository<Review>().UpdateAsync(review);
            _unitOfWork.Complete();
            
        }
    }
}

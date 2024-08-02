using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        public DeleteRatingCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;            
        }
        public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var ratingToDelete = await _unitOfWork.Repository<Rating>().GetByCondition(x => x.EmployerId == request.EmployerId&&x.JobSeekerId==jobSeeker.Id).FirstOrDefaultAsync();
            if (ratingToDelete == null)
            {
                throw new Exception("Rating not found or you are not authorized to delete!");
            }
            _unitOfWork.Repository<Rating>().Delete(ratingToDelete);
            _unitOfWork.Complete();
            return true;
        }
    }
}

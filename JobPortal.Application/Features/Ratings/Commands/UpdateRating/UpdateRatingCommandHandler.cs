using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public UpdateRatingCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;            
        }
        public async Task Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var ratingToUpdate = await _unitOfWork.Repository<Rating>().GetByCondition(x => x.EmployerId == request.EmployerId && x.JobSeekerId == jobSeeker.Id).FirstOrDefaultAsync();
            if (ratingToUpdate == null)
            {
                throw new Exception("Rating not foud or you are not authorized");
            }
            ratingToUpdate.Score = request.Score;
            await _unitOfWork.Repository<Rating>().UpdateAsync(ratingToUpdate);
            _unitOfWork.Complete();

        }
    }
}

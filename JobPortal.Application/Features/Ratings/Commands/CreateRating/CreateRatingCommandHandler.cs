using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        

        public CreateRatingCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
      
        }
        public async Task Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var employer=await _unitOfWork.Repository<Employer>().GetByIdAsync(x=>x.Id==request.EmployerId);
            if(employer == null)
            {
                throw new Exception("employer does not exists");
            }

            try
            {
                var ratingToCreate = new Rating
                {
                    JobSeekerId = jobSeeker.Id,
                    EmployerId = request.EmployerId,
                    Score = request.Score,
                };

                await _unitOfWork.Repository<Rating>().CreateAsync(ratingToCreate);
                _unitOfWork.Complete();
            }
            catch (Exception ex) 
            {
                throw new Exception("A rating from this job seeker for this employer already exists.");

            }
        }
    }
}

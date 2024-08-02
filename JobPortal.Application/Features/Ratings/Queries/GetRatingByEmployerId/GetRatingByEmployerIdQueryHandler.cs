using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Ratings.Queries.GetRatingByEmployerId
{
    public class GetRatingByEmployerIdQueryHandler : IRequestHandler<GetRatingByEmployerIdQuery, double>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetRatingByEmployerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<double> Handle(GetRatingByEmployerIdQuery request, CancellationToken cancellationToken)
        {
            var employer=_unitOfWork.Repository<Employer>().GetByIdAsync(x=>x.Id==request.EmployerId);
            if (employer == null) 
            {
                throw new Exception("Employer not foud!");
            
            }
            var averageRating = await _unitOfWork.Repository<Rating>()
                .GetByCondition(x=>x.EmployerId==request.EmployerId).AverageAsync(x=>x.Score);
            var roundedAverageRating = Math.Round(averageRating, 2);
            return roundedAverageRating;
        }
    }
}

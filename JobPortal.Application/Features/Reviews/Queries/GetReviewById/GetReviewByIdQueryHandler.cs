using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Reviews.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetReviewByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.Repository<Review>().GetByCondition(x => x.Id == request.Id).Include(x=>x.JobSeeker).FirstOrDefaultAsync();
            if (review == null)
            {
                throw new Exception("Review not found!");
            }
            var reviewToReturn=_mapper.Map<ReviewDto>(review);
          

            return reviewToReturn;

        }
    }
}

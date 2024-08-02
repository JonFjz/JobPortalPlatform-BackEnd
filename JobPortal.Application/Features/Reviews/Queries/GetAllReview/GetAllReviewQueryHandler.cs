using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Reviews.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Reviews.Queries.GetAllReview
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, PagedResult<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllReviewQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReviewDto>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var totalCount = await _unitOfWork.Repository<Review>()
               .CountByConditionAsync(x => x.EmployerId == request.EmployerId);

            var reviews = await _unitOfWork.Repository<Review>()
                .GetByCondition(x => x.EmployerId == request.EmployerId)
                .Include(x=>x.JobSeeker)
                .OrderByDescending(x=>x.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var reviewsToReturn=_mapper.Map<List<ReviewDto>>(reviews);

            return new PagedResult<ReviewDto>
            {
                Items = reviewsToReturn,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

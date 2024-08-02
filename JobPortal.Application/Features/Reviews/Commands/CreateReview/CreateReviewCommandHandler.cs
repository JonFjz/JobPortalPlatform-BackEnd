using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using JobPortal.Application.Features.Reviews.Dtos;

namespace JobPortal.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IRealTimeService _realTimeService;
        
        public CreateReviewCommandHandler(IMapper mapper,IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor,IRealTimeService realTimeService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _realTimeService = realTimeService;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var reviewToCreate = new Review
            {
                Content = request.Content,
                JobSeeker = jobSeeker,
                EmployerId = request.EmployerId
            };

            var reviewDto = _mapper.Map<ReviewDto>(reviewToCreate);
            await _unitOfWork.Repository<Review>().CreateAsync(reviewToCreate);
            _unitOfWork.Complete();
            await _realTimeService.NotifyNewReview(request.EmployerId, reviewDto);
            return reviewToCreate.Id;
        }
    }
}

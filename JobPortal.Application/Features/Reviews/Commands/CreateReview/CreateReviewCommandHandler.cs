using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        
        public CreateReviewCommandHandler(IMapper mapper,IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
           
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
            

            await _unitOfWork.Repository<Review>().CreateAsync(reviewToCreate);
            _unitOfWork.Complete(); 
            return reviewToCreate.Id;
        }
    }
}

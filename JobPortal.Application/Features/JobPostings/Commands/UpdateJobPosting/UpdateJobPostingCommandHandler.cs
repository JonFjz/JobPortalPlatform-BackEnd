using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting
{
    public class UpdateJobPostingCommandHandler : IRequestHandler<UpdateJobPostingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public UpdateJobPostingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.Id);

            if (jobPosting == null || jobPosting.EmployerId != employer.Id)
            {
                throw new KeyNotFoundException("Job Posting not found.");
            }

            _mapper.Map(request, jobPosting);

            await _unitOfWork.Repository<JobPosting>().UpdateAsync(jobPosting);
            _unitOfWork.Complete();

        }
    }
}
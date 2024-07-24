using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.DeleteJobPosting
{
    internal class DeleteJobPostingCommandHandler : IRequestHandler<DeleteJobPostingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public DeleteJobPostingCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var jobPosting = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(e => e.Id == request.Id);

            if (jobPosting == null || jobPosting.EmployerId != employer.Id)
                throw new KeyNotFoundException("Job Posting not found.");

            _unitOfWork.Repository<JobPosting>().Delete(jobPosting);
            _unitOfWork.Complete();
        }
    }
}

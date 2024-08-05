using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.JobApplications.Command.UpdateJobApplicationStatus
{
    public class UpdateJobApplicationStatusCommandHandler : IRequestHandler<UpdateJobApplicationStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public UpdateJobApplicationStatusCommandHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }

        public async Task Handle(UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();

            var jobApplication = await _unitOfWork.Repository<JobApplication>()
                .GetByCondition(e => e.Id == request.JobApplicationId)
                .Include(x => x.JobPosting).FirstOrDefaultAsync();
                

            if (jobApplication == null)
            {
                throw new KeyNotFoundException("Job application not found.");
            }

            if (jobApplication.JobPosting.EmployerId != employer.Id)
            {
                throw new Exception("You can not update status of this application");
            }

            jobApplication.JobApplicationStatus = request.NewStatus;

            await _unitOfWork.Repository<JobApplication>().UpdateAsync(jobApplication);
            _unitOfWork.Complete();
        }
    }
}

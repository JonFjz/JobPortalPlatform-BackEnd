using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Command.UpdateJobApplicationStatus
{
    public class UpdateJobApplicationStatusCommandHandler : IRequestHandler<UpdateJobApplicationStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateJobApplicationStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var jobApplication = await _unitOfWork.Repository<JobApplication>().GetByIdAsync(e => e.Id == request.JobApplicationId);

            if (jobApplication == null)
            {
                throw new KeyNotFoundException("Job application not found.");
            }

            jobApplication.JobApplicationStatus = request.NewStatus;

            await _unitOfWork.Repository<JobApplication>().UpdateAsync(jobApplication);
            _unitOfWork.Complete();
        }
    }
}

using JobPortal.Domain.Enums;
using MediatR;

namespace JobPortal.Application.Features.JobApplications.Command.UpdateJobApplicationStatus
{
    public class UpdateJobApplicationStatusCommand : IRequest
    {
        public int JobApplicationId { get; set; }
        public JobApplicationStatus NewStatus { get; set; }
    }
}

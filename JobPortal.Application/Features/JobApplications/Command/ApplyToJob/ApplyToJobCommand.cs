using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Application.Features.JobApplications.Command.ApplyToJob
{
    public class ApplyToJobCommand : IRequest<int>
    {
        public int JobPostingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ResumeId { get; set; } 
        public IFormFile NewResumeFile { get; set; }
    }
}

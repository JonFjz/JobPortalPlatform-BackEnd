using JobPortal.Application.Features.JobPostings.Dtos;

namespace JobPortal.Application.Features.JobApplications.Dtos
{
    public class JobApplicatinForJobSeekerDto
    {
        public int Id { get; set; }
        public DateTime AppliedOn { get; set; } 
        public string JobApplicationStatus { get; set; }

        public JobPostingDto JobPosting { get; set; }
    }
}

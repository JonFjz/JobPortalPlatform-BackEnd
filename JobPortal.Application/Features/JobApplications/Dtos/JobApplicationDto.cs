namespace JobPortal.Application.Features.JobApplications.Dtos
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AppliedOn { get; set; }
        public string JobApplicationStatus { get; set; }
        public string ResumeOriginalName { get; set; }
    }
}

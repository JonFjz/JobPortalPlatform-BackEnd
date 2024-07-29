using JobPortal.Domain.Enums;

namespace JobPortal.Domain.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public DateTime AppliedOn { get; set; } = DateTime.Now;
        public JobApplicationStatus JobApplicationStatus { get; set; } = JobApplicationStatus.Pending;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SnapshotResumeBlobName { get; set; }
        public string ResumeOriginalName { get; set; }


        public int JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; }
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
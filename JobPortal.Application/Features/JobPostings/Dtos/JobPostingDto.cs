namespace JobPortal.Application.Features.JobPostings.Dtos
{
    public class JobPostingDto
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public String CompanyName { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredSkills { get; set; }
        public string WorkType { get; set; }
        public string WorkLevel { get; set; }

    }
}

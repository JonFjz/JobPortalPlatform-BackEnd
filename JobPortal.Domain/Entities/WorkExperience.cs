namespace JobPortal.Domain.Entities
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }

        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}

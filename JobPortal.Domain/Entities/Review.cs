namespace JobPortal.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}

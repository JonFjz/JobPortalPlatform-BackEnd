namespace JobPortal.Domain.Entities
{
    public class JobPosting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }

    }
}

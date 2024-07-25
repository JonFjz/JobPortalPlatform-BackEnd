namespace JobPortal.Domain.Entities
{
    public class BookmarkJob
    {
        
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public int JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; }
    }
}

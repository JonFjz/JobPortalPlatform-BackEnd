namespace JobPortal.Domain.Entities
{
    public class Resume
    {
        public int Id { get; set; }
        public string BlobUniqueName { get; set; }
        public string OriginalResumeName { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public int JobSeekerId { get; set; }
       
        // [JsonIgnore]
        public JobSeeker JobSeeker { get; set; }
    }
}

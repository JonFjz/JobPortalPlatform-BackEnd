using System.Text.Json.Serialization;

namespace JobPortal.Domain.Entities
{
    public class Resume
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ResumeName { get; set; }
        public DateTime UploadedAt { get; set; }

        public int JobSeekerId { get; set; }
        
        [JsonIgnore]
        public JobSeeker JobSeeker { get; set; }
    }
}

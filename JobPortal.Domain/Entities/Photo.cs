namespace JobPortal.Domain.Entities
{
    public class Photo
	{
        public int Id { get; set; }
        public string BlobUniqueName { get; set; }
        public string OriginalPhotoName { get; set; }
        public DateTime UploadedAt { get; set; }

        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}


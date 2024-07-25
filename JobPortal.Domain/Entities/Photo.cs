using System;
namespace JobPortal.Domain.Entities
{
	public class Photo
	{
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime UploadedAt { get; set; }

        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}


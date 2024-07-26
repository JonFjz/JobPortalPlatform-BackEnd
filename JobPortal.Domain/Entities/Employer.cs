using JobPortal.Domain.Enums;

namespace JobPortal.Domain.Entities
{
    public class Employer
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; } // Auth0 User ID
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Founded { get; set; }
        public int CompanySize { get; set; }
        public string CompanyLink{ get; set; }
        public Industry Industry { get; set; }
        public string Description { get; set; }
        public Photo Photo { get; set; }

        public ICollection<JobPosting> JobPostings { get; set; }
       
    }
}

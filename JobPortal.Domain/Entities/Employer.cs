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
        public string Industry { get; set; } // Industry the company operates in
        public string Description { get; set; }
    }
}

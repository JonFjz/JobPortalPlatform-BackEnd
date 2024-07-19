namespace JobPortal.Domain.Entities
{
    public class Employer
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; } // Auth0 User ID
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyAddress { get; set; }

    }
}

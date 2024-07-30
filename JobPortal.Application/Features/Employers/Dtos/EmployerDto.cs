namespace JobPortal.Application.Features.Employers.Dtos
{
    public class EmployerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Founded { get; set; }
        public int CompanySize { get; set; }
        public string CompanyLink { get; set; }
        public string Industry { get; set; } 
        public string Description { get; set; }
    }
}

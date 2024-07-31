using JobPortal.Domain.Enums;

namespace JobPortal.Application.Features.Employers.Dtos
{
    public class EmployerRegistrationDto
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Industry Industry { get; set; }
        public string Password { get; set; }
    }
}

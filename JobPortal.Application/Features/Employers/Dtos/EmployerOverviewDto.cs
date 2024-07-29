namespace JobPortal.Application.Features.Employers.Dtos
{
    public class EmployerOverviewDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int CompanySize { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
    }
}

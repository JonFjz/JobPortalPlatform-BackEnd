using MediatR;

namespace JobPortal.Application.Features.Educations.Commands.UpdateEducation
{
    public class UpdateEducationCommand : IRequest
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string InstitutionName { get; set; }
    }
}

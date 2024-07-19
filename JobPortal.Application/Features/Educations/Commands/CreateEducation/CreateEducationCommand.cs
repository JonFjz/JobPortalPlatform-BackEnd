using MediatR;

namespace JobPortal.Application.Features.Educations.Commands.CreateEducation
{
    public class CreateEducationCommand : IRequest<int>
    {
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string InstitutionName { get; set; }

    }
}

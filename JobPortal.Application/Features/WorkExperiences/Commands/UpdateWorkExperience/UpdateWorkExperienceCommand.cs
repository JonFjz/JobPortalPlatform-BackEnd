using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommand : IRequest
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
    }
}

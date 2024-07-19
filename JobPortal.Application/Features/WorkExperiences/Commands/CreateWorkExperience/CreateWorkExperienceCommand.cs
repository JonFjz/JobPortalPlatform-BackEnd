using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience
{
    public class CreateWorkExperienceCommand : IRequest<int>
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
    }
}

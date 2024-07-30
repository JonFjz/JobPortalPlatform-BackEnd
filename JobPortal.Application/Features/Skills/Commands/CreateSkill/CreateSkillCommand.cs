using MediatR;

namespace JobPortal.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string YearsOfExperience { get; set; }

    }
}

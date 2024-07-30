using MediatR;

namespace JobPortal.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string YearsOfExperience { get; set; }

    }
}

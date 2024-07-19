using MediatR;

namespace JobPortal.Application.Features.WorkExperiences.Commands.DeleteWorkExperience
{
    public class DeleteWorkExperienceCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteWorkExperienceCommand(int id)
        {
            Id = id;
        }
    }
}
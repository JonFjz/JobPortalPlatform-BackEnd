using MediatR;

namespace JobPortal.Application.Features.Educations.Commands.DeleteEducation
{
    public class DeleteEducationCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteEducationCommand(int id)
        {
            Id = id;
        }
    }
}
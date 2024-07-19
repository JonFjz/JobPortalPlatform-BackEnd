using MediatR;

namespace JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker
{
    public class UpdateJobSeekerProfileCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}

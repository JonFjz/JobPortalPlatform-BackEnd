using JobPortal.Domain.Enums;
using MediatR;

namespace JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile
{
    public class UpdateEmployerProfileCommand : IRequest
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int Founded { get; set; }
        public int CompanySize { get; set; }
        public string CompanyLink { get; set; }
        public Industry Industry { get; set; }
        public string Description { get; set; }
    }

}

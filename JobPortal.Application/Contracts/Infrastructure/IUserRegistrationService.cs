using JobPortal.Application.Features.Users.Dtos;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IUserRegistrationService
    {
        Task SaveJobSeekerAsync(JobSeekerRegistrationDto request, string auth0Id);
        Task SaveEmployerAsync(EmployerRegistrationDto request, string auth0Id);
    }

}

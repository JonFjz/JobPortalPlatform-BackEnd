using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobSeeker.Dtos;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IUserRegistrationService
    {
        Task SaveJobSeekerAsync(JobSeekerRegistrationDto request, string auth0Id);
        Task SaveEmployerAsync(EmployerRegistrationDto request, string auth0Id);
    }
}

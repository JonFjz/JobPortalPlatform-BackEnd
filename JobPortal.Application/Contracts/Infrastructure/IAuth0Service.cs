using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Application.Helpers.Models.Auth0;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IAuth0Service
    {
        Task<string> SignupJobSeekerAsync(JobSeekerRegistrationDto request, string userType);
        Task<string> SignupEmployerAsync(EmployerRegistrationDto signupRequest, string userType);
        Task<string> GetTokenAsync(Auth0TokenRequest tokenRequest);

    }
}
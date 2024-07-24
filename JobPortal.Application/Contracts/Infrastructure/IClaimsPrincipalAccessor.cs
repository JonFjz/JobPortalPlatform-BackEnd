using JobPortal.Domain.Entities;
using System.Security.Claims;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
        Task<JobSeeker> GetCurrentJobSeekerAsync();
        Task<Employer> GetCurrentEmployerAsync();

    }
}

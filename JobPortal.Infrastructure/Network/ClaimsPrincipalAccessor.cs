using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JobPortal.Infrastructure.Network
{
    public class ClaimsPrincipalAccessor : IClaimsPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ClaimsPrincipalAccessor(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;


        private string GetUserId()
        {
            var userId = ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }
            return userId;
        }

        public async Task<JobSeeker> GetCurrentJobSeekerAsync()
        {
            var userId = GetUserId();
            var jobSeeker = (await _unitOfWork.Repository<JobSeeker>()
                .GetByConditionAsync(js => js.Auth0Id == userId))
                .FirstOrDefault();

            if (jobSeeker == null)
            {
                throw new KeyNotFoundException("Job seeker not found.");
            }

            return jobSeeker;
        }

        public async Task<Employer> GetCurrentEmployerAsync()
        {
            var userId = GetUserId();
            var employer = (await _unitOfWork.Repository<Employer>()
                .GetByConditionAsync(e => e.Auth0Id == userId))
                .FirstOrDefault();

            if (employer == null)
            {
                throw new KeyNotFoundException("Employer not found.");
            }

            return employer;
        }
    }
}

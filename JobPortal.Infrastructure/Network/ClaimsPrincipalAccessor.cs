using JobPortal.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JobPortal.Infrastructure.Network
{
    public class ClaimsPrincipalAccessor : IClaimsPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;

    }
}

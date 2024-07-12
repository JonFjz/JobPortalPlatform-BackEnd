using JobPortal.Application.Helpers.Models.Auth0;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IAuth0Service
    {
        Task<string> SignupUserAsync(Auth0SignupRequest signupRequest, string userType);
        Task<string> GetTokenAsync(Auth0TokenRequest tokenRequest);

    }
}

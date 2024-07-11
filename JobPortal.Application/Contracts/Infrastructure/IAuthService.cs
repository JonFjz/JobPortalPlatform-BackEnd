

using JobPortal.Application.Features.Users.Dtos;
using JobPortal.Application.Responses;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterDto registerDto);
        Task<AuthResponse> Login(LoginDto loginDto);
    }
}

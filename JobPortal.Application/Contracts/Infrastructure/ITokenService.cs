using JobPortal.Domain.Entities.User;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}

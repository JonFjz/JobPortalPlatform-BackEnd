namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ICacheService
    {
        Task CacheResponseAsync(string key, object response, TimeSpan expiration);
        Task<string> GetCachedResponse(string key);
    }
}

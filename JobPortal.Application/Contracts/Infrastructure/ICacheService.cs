namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ICacheService
    {

        Task SetCacheAsync<T>(string key, T value, TimeSpan expiration);
        Task<T> GetCacheAsync<T>(string key);
    }
}

using JobPortal.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace JobPortal.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializeValue=JsonConvert.SerializeObject(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            await _cache.SetStringAsync(key, serializeValue, options);

           
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {

            var deserializeValue=JsonConvert.DeserializeObject<T>( await _cache.GetStringAsync(key));
            if (deserializeValue == null)
            { 
                return default;
            }
            return deserializeValue;
        }
    }
}

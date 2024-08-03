using JobPortal.Application.Contracts.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;

namespace JobPortal.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string key, object response, TimeSpan expiry)
        {
            if (response == null) return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(key, serialisedResponse, expiry);
        }

        public async Task<string> GetCachedResponse(string key)
        {
            var cachedResponse = await _database.StringGetAsync(key);

            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }
    }
}
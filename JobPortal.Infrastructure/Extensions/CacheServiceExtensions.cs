using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Infrastructure.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Extensions
{
    public static class CacheServiceExtensions
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Redis Cache
            var redisConnectionString = configuration.GetSection("Redis:ConnectionString").Value;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "JPPInstance";
            });

            // Register CacheService as Singleton
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}

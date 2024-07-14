using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface ICacheService
    {

        Task SetCacheAsync<T>(string key, T value, TimeSpan expiration);
        Task<T> GetCacheAsync<T>(string key);
    }
}

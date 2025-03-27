using Artexitus.IdentityMicroservice.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Artexitus.IdentityMicroservice.Infrastructure.Persistence.Repositories
{
    public class RedisCacheAccessor : ICacheAccessor
    {
        private readonly IDistributedCache _cache;

        public RedisCacheAccessor(IDistributedCache cache)
        {
            _cache = cache;
        }

        public TEntity? Get<TEntity>(string key)
        {
            var json = _cache.GetString(key);

            if (json == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<TEntity>(json);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, object payload, TimeSpan lifetime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = lifetime
            };

            _cache.SetString(key, JsonSerializer.Serialize(payload), options);
        }
    }
}

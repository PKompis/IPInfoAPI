using IPInfo.Core.Services;
using IPInfo.Services.Configuration;
using System;
using System.Runtime.Caching;

namespace IPInfo.Services
{
    public class CachingService : ICachingService
    {
        private MemoryCache _cache;
        private readonly ExpirationConfiguration _expirationConfiguration;

        public CachingService(ExpirationConfiguration expirationConfiguration)
        {
            _cache = new MemoryCache("IPInfoAPI");
            _expirationConfiguration = expirationConfiguration;
        }

        public bool Add<T>(string key, T value, DateTimeOffset? expiration = null)
        {
            CacheItemPolicy cacheItemPolicy = null;

            if (expiration != null) cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = expiration.Value };

            return _cache.Add(new CacheItem(key, value), cacheItemPolicy ?? GetCacheItemPolicy());
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public bool Remove(string key)
        {
            return _cache.Remove(key) != null;
        }

        public T Get<T>(string key)
        {
            var value = _cache.Get(key);
            return value is T obj ? obj : default;
        }
        private CacheItemPolicy GetCacheItemPolicy() => new CacheItemPolicy
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(_expirationConfiguration.ExpirationMinutes.GetValueOrDefault())
        };
    }
}

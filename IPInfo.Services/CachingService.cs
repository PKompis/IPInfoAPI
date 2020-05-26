using IPInfo.Core.Services;
using System;
using System.Runtime.Caching;

namespace IPInfo.Services
{
    public class CachingService : ICachingService
    {
        private readonly CacheItemPolicy _cacheItemPolicy;
        private MemoryCache _cache;

        public CachingService(CacheItemPolicy cacheItemPolicy)
        {
            _cacheItemPolicy = cacheItemPolicy;
            _cache = new MemoryCache("IPInfoAPI");
        }

        public bool Add<T>(string key, T value, DateTimeOffset? expiration = null)
        {
            CacheItemPolicy cacheItemPolicy = null;

            if (expiration != null) cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = expiration.Value };

            return _cache.Add(new CacheItem(key, value), cacheItemPolicy ?? _cacheItemPolicy);
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
    }
}

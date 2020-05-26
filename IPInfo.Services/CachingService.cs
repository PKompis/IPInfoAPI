using IPInfo.Core.Services;
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

        public bool Add<T>(string key, T value)
        {
            return _cache.Add(new CacheItem(key, value), _cacheItemPolicy);
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public T Get<T>(string key)
        {
            var value = _cache.Get(key);
            return value is T obj ? obj : default;
        }
    }
}

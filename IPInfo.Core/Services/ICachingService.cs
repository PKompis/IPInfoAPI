using System;

namespace IPInfo.Core.Services
{
    public interface ICachingService
    {
        bool Contains(string key);
        bool Add<T>(string key, T value, DateTimeOffset? expiration = null);
        T Get<T>(string key);
        bool Remove(string key);
    }
}

namespace IPInfo.Core.Services
{
    public interface ICachingService
    {
        bool Contains(string key);
        bool Add<T>(string key, T value);
        T Get<T>(string key);
    }
}

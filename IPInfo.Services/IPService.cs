using IPInfo.Core.Exceptions;
using IPInfo.Core.Services;
using IPInfo.Library.Interfaces;
using System.Threading.Tasks;

namespace IPInfo.Services
{
    public class IPService : IIPService
    {
        private readonly IIPInfoProvider _ipInfoProvider;
        private readonly ICachingService _cachingService;

        private const string NotValidIPException = "IP Address is not Valid";

        public IPService(IIPInfoProvider iPInfoProvider, ICachingService cachingService)
        {
            _ipInfoProvider = iPInfoProvider;
            _cachingService = cachingService;
        }

        public async Task<IPDetails> GetIPDetails(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip) || !System.Net.IPAddress.TryParse(ip, out _)) throw new BadRequestException(NotValidIPException);

            if (_cachingService.Contains(ip))
            {
                var ipDetails = _cachingService.Get<IPDetails>(ip);
                return ipDetails ?? _ipInfoProvider.GetIPDetails(ip);
            }

            var value = _ipInfoProvider.GetIPDetails(ip);
            _cachingService.Add(ip, value);

            return value;
        }

    }
}

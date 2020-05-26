using IPInfo.Core.Exceptions;
using IPInfo.Core.Services;
using IPInfo.Library.Interfaces;
using System.Threading.Tasks;

namespace IPInfo.Services
{
    public class IPService : IIPService
    {
        private readonly IIPInfoProvider _ipInfoProvider;

        private const string NotValidIPException = "IP Address is not Valid";

        public IPService(IIPInfoProvider iPInfoProvider)
        {
            _ipInfoProvider = iPInfoProvider;
        }

        public async Task<IPDetails> GetIPDetails(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip) || !System.Net.IPAddress.TryParse(ip, out _)) throw new BadRequestException(NotValidIPException);

            return _ipInfoProvider.GetIPDetails(ip);
        }

    }
}

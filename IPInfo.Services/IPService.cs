using IPInfo.Core;
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
        private readonly IUnitOfWork _unitOfWork;

        private const string NotValidIPException = "IP Address is not Valid";

        public IPService(IIPInfoProvider iPInfoProvider, ICachingService cachingService, IUnitOfWork unitOfWork)
        {
            _ipInfoProvider = iPInfoProvider;
            _cachingService = cachingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IPDetails> GetIPDetails(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip) || !System.Net.IPAddress.TryParse(ip, out _)) throw new BadRequestException(NotValidIPException);

            if (_cachingService.Contains(ip))
            {
                var result = _cachingService.Get<IPDetails>(ip);
                if (result != null) return result;
            }

            var dbIpDetails = await _unitOfWork.IP.GetIPDetailsAsync(ip);
            if (dbIpDetails != null)
            {
                _cachingService.Add(ip, (IPDetails)dbIpDetails);
                return dbIpDetails;
            }

            var ipDetails = _ipInfoProvider.GetIPDetails(ip);
            await _unitOfWork.IP.AddAsync(ipDetails?.ToDbIpDetails(ip));
            await _unitOfWork.CommitAsync();

            _cachingService.Add(ip, ipDetails);

            return ipDetails;
        }

    }
}

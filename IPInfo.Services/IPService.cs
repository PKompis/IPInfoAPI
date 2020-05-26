using Hangfire;
using IPInfo.Core;
using IPInfo.Core.Exceptions;
using IPInfo.Core.Services;
using IPInfo.Library.Interfaces;
using IPInfo.Services.HelperTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPInfo.Services
{
    public class IPService : IIPService
    {
        private readonly IIPInfoProvider _ipInfoProvider;
        private readonly ICachingService _cachingService;
        private readonly IUnitOfWork _unitOfWork;

        private const string NotValidIPException = "IP Address is not Valid";
        private const string NotValidId = "Identifier is not Valid";
        private const string DoesNotExistsOrCompleted = "The Job either does not Exist or it has been Completed";

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

        public string GetBatchProgress(Guid? id)
        {
            if (id == null) throw new BadRequestException(NotValidId);

            var cacheItem = _cachingService.Get<BatchJobDetails>(id.ToString());

            if(cacheItem == null) throw new BadRequestException(DoesNotExistsOrCompleted);

            return cacheItem.ProcessedItems + "/" + cacheItem.TotalItems;
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task PostIpDetailsList(IEnumerable<IPFullDetails> ipDetailsList, Guid trackingGuid)
        {
            var ipList = _unitOfWork.IP.GetNonExistingIPs(ipDetailsList?.ToDistinctIpDomainModelList())?.ToList();

            var cacheExpirationMinutes = DateTimeOffset.Now.AddMinutes(1440);

            if (!_cachingService.Contains(trackingGuid.ToString())) _cachingService.Add(trackingGuid.ToString(), ipList.Count().RetrieveBatchJobDetails(0), cacheExpirationMinutes);

            while (ipList.Count > 0)
            {
                var itemsToProcess = ipList.Take(10)?.ToList();
                ipList.RemoveRange(0, itemsToProcess.Count());
                await _unitOfWork.IP.AddRangeAsync(itemsToProcess);
                await _unitOfWork.CommitAsync();
                var progress = _cachingService.Get<BatchJobDetails>(trackingGuid.ToString());
                progress.ProcessedItems += itemsToProcess.Count();
                _cachingService.Remove(trackingGuid.ToString());
                _cachingService.Add(trackingGuid.ToString(), progress, cacheExpirationMinutes);
            }
        }


    }
}

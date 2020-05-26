using IPInfo.Core.Models;
using IPInfo.Library.Interfaces;
using IPInfo.Services.HelperTypes;
using System.Collections.Generic;
using System.Linq;

namespace IPInfo.Services
{
    internal static class Extensions
    {
        internal static IP ToDbIpDetails(this IPDetails ipDetails, string ip) => new IP
        {
            Ip = ip,
            City = ipDetails.City,
            Continent = ipDetails.Continent,
            Country = ipDetails.Country,
            Latitude = ipDetails.Latitude,
            Longitude = ipDetails.Longitude
        };

        internal static BatchJobDetails RetrieveBatchJobDetails(this int totalItems, int processedItems) => new BatchJobDetails
        {
            ProcessedItems = processedItems,
            TotalItems = totalItems
        };

        internal static IEnumerable<IP> ToDistinctIpDomainModelList(this IEnumerable<IPFullDetails> ipDetailsList)
        {
            var listToReturn = new List<IP>();

            foreach (var item in ipDetailsList)
            {
                if (listToReturn?.Any(x => x.Ip == item.Ip) == false) listToReturn.Add(item.ToIpDomainModel());
            }

            return listToReturn;
        }

        internal static IP ToIpDomainModel(this IPFullDetails ipDetails) => new IP
        {
            Ip = ipDetails.Ip,
            City = ipDetails.City,
            Continent = ipDetails.Continent,
            Country = ipDetails.Country,
            Latitude = ipDetails.Latitude,
            Longitude = ipDetails.Longitude
        };

    }
}

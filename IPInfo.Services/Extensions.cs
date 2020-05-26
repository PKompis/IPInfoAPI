using IPInfo.Core.Models;
using IPInfo.Library.Interfaces;

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
    }
}

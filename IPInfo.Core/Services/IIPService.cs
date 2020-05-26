using IPInfo.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPInfo.Core.Services
{
    public interface IIPService
    {
        Task<IPDetails> GetIPDetails(string ip);
        string GetBatchProgress(Guid? id);
        Task PostIpDetailsList(IEnumerable<IPFullDetails> ipDetailsList, Guid trackingGuid);
    }
}

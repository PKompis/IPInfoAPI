using IPInfo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPInfo.Core.Repositories
{
    public interface IIPRepository : IRepository<IP>
    {
        Task<IP> GetIPDetailsAsync(string ip);
        IEnumerable<IP> GetNonExistingIPs(IEnumerable<IP> ipList);
    }
}

using IPInfo.Core.Models;
using IPInfo.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPInfo.Data.Repositories
{
    public class IPRepository : Repository<IP>, IIPRepository
    {
        public IPRepository(IPInfoDbContext context) : base(context) { }


        public async Task<IP> GetIPDetailsAsync(string ip)
        {
            return await IPInfoDbContext.IPDetails
                .SingleOrDefaultAsync(i => i.Ip == ip);
        }

        public IEnumerable<IP> GetNonExistingIPs(IEnumerable<IP> ipList)
        {
            return ipList?.Where(x => !IPInfoDbContext.IPDetails.Any(y => y.Ip == x.Ip))?.ToList();
        }


        private IPInfoDbContext IPInfoDbContext
        {
            get { return Context as IPInfoDbContext; }
        }
    }
}

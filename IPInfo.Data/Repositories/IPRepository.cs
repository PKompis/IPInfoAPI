using IPInfo.Core.Models;
using IPInfo.Core.Repositories;
using Microsoft.EntityFrameworkCore;
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

        private IPInfoDbContext IPInfoDbContext
        {
            get { return Context as IPInfoDbContext; }
        }
    }
}

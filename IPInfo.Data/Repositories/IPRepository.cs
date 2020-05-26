using IPInfo.Core.Models;
using IPInfo.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace IPInfo.Data.Repositories
{
    public class IPRepository : Repository<IP>, IIPRepository
    {
        public IPRepository(IPInfoDbContext context) : base(context) { }


        public async Task<IP> GetIPDetails(string ip)
        {
            throw new NotImplementedException();
        }

        private IPInfoDbContext IPInfoDbContext
        {
            get { return Context as IPInfoDbContext; }
        }
    }
}
